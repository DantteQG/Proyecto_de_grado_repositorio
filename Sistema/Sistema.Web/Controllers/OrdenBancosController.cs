using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Solicitud;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Sistema.Web.Models.Solicitud.OrdenBanco;
using System.Security.Cryptography;
using Sistema.Web.Models.Credinet.Aprobador;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenBancosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public OrdenBancosController(DbContextSistema context)
        {
            _context = context;
        }



        // POST: api/OrdenBancos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //Recibimos el Json desde el front end con los datos necesarios para el banco
        [HttpPost("[action]")]
        public async Task<ActionResult> InfoBancoProesa([FromBody] OrdenBancoViewModel model)
        {
            //verificamos el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<CismartApprover> aprobadores;
            //la lista de aprobadores es proporiconada por el banco
            aprobadores = new List<CismartApprover>
           {
                new CismartApprover
                {
                    idc = "00255921-Q-LP 1A",//aprobador1
                    type = 1
                },
           };

            List<FormAchPayment> pagoACH;
            List<FormProvidersPayment> pagoBCP;
            //verificamos si en un ACH o propio bcp por el codigo del banco
            if (model.codigobanco == 12)
            {
                pagoBCP = new List<FormProvidersPayment>
                {
                    new FormProvidersPayment
                        {
                            paymentType = "PROV",  //PAGO A BCP
                            line = 1,
                            accountNumber = model.nrocuenta,  //NUMERO DE CUENTA
                            glossPayment = model.concepto,   //CASO ****
                            amount = model.monto,           //MONTO 
                            documentType = "Q",
                            documentNumber = "0", //CI:0
                            documentExtension = "LP",    // LP
                            firstDetail = "",
                            secondDetail = "",
                            mail = ""   //MAIL DE USUARIO
                        }
                };
                pagoACH = new List<FormAchPayment>();
            }
            else
            {
                pagoACH = new List<FormAchPayment>
                {
                    new FormAchPayment
                        {
                            paymentType = "ACH",  //PAGO ACH
                            line = 1,
                            accountNumber = model.nrocuenta, //NUMERO DE CUENTA
                            titularName = model.nombretitular,  //NOMBRE DE CUENTA
                            firstLastName= "",
                            amount = model.monto,       //MONTO
                            branchOfficeId = 201,       //SUCURSAL
                            mail = "",
                            bankId = model.codigobanco     //ID DEL BANCO DESTINO
                        }
                };
                pagoBCP = new List<FormProvidersPayment>();
            }
            //llenamos el Json completo con los datos anteriores
            var requestDatas = new RequestData
            {
                companyId = 2295,
                password = "pato123AB",
                documentNumber = "4850147", //NIT EMPRESA
                documentType = "Q",
                documentExtension = "LP",
                documentComplement = "",
                amount = model.monto, //MONTO
                currency = "BOL", //MONEDA
                fundSource = "FONDOS POR VENTA DE PRODUCTOS",
                fundDestination = model.concepto,  //DESTINO DE FONDOS
                sourceAccount = "2011040905323", //CUENTA SALIDA
                sourceCurrency = "BOL", //MONEDA CUENTA
                cismartApprovers = aprobadores,
                spreadsheet = new Spreadsheet
                {
                    formOddPayments = new List<FormOddPayment>(),
                    formProvidersPayments = pagoBCP,
                    formAchPayments = pagoACH
                }
            };

            // Serializar el objeto RequestData a una cadena JSON
            string jsonDatabanco = JsonConvert.SerializeObject(requestDatas);
            //Encriptar la cadena JSON
            string jsonDataEncrip = Encrip.Encrypt(jsonDatabanco);
            //Enviar informacion al destino mediante una tarea asincrona,
            //con los datos proporcionados por el banco
            string response = await PostDataAsync(jsonDataEncrip);
            //Desencriptar la respuesta del banco
            string responseDecryp = Encrip.Decrypt(response);
            // Deserializar la respuesta y dividir
            ApiResponse responseJson = JsonConvert.DeserializeObject<ApiResponse>(responseDecryp);

            string result = responseJson.Result;
            string code = responseJson.Code;
            string message = responseJson.Message;
            //verificar que la respuesta no sea nula
            if (response == null)
            {
                return BadRequest();
            }
            //Enviar la respuesta al Front End 
            return Ok(new { lote = result, code = code, message = message });
        }

        private static async Task<string> PostDataAsync(string jsonDataBanco)
        {
            string url = "https://www99.bancred.com.bo/ApiGatewayCw/APIAuthentication/api/APIAuthentication/PreparerBatch";

            using (var httpClientHandler = new HttpClientHandler())
            {

                string username = "pato123";
                string password = "pato123AB";
                string companyID = "2295";

                string currentDirectory = Directory.GetCurrentDirectory();

                // Construir la ruta completa al certificado PFX
                string certPath = Path.Combine(currentDirectory, "cert", "BCPCW_API_EMPRESA.pfx");


                var requestData = new
                {
                    companyId = companyID, // Código de compañía proporcionado por el banco
                    data = jsonDataBanco   // Información encriptada
                };

                // Serializar el objeto JSON a una cadena
                string jsonData = JsonConvert.SerializeObject(requestData);

                // Cargar el certificado PFX desde el archivo
                X509Certificate2 certificate = new X509Certificate2(certPath, "abc123");

                // Agregar el certificado a la solicitud HTTP para autenticación
                httpClientHandler.ClientCertificates.Add(certificate);


                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    // Codificar las credenciales en base64
                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                    // Agregar el encabezado Authorization a la solicitud
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                    // Agregar otros encabezados si es necesario
                    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    httpClient.DefaultRequestHeaders.Add("Correlation-Id", "123");

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await httpClient.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();

                        return await response.Content.ReadAsStringAsync();
                    }
                    catch (HttpRequestException e)
                    {
                        // Manejar el error si la solicitud no es exitosa
                        //Console.WriteLine($"Error: {e.Message}");
                        return null;
                    }
                }
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InfoBancoLog([FromBody] OrdenBancoViewModel model)
        {
            //verificamos el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<CismartApprover> aprobadores;
            //la lista de aprobadores es proporiconada por el banco
            aprobadores = new List<CismartApprover>
           {
                new CismartApprover
                {
                    idc = "2315726-Q-LP",//aprobador1 KR
                    type = 1
                },
                //new CismartApprover
                //{
                //    idc = "466517-Q-LP 3C",//aprobador1 LR
                //    type = 3
                //},
           };

            List<FormAchPayment> pagoACH;
            List<FormProvidersPayment> pagoBCP;
            //verificamos si en un ACH o propio bcp por el codigo del banco
            if (model.codigobanco == 12)
            {
                pagoBCP = new List<FormProvidersPayment>
                {
                    new FormProvidersPayment
                        {
                            paymentType = "PROV",  //PAGO A BCP
                            line = 1,
                            accountNumber = model.nrocuenta,  //NUMERO DE CUENTA
                            glossPayment = model.concepto,   //CASO ****
                            amount = model.monto,           //MONTO 
                            documentType = "Q",
                            documentNumber = "0", //CI:0
                            documentExtension = "LP",    // LP
                            firstDetail = "",
                            secondDetail = "",
                            mail = ""   //MAIL DE USUARIO
                        }
                };
                pagoACH = new List<FormAchPayment>();
            }
            else
            {
                pagoACH = new List<FormAchPayment>
                {
                    new FormAchPayment
                        {
                            paymentType = "ACH",  //PAGO ACH
                            line = 1,
                            accountNumber = model.nrocuenta, //NUMERO DE CUENTA
                            titularName = model.nombretitular,  //NOMBRE DE CUENTA
                            firstLastName= "",
                            amount = model.monto,       //MONTO
                            branchOfficeId = 201,       //SUCURSAL
                            mail = "",
                            bankId = model.codigobanco            //ID DEL BANCO DESTINO
                        }
                };
                pagoBCP = new List<FormProvidersPayment>();
            }
            //llenamos el Json completo con los datos anteriores
            var requestDatas = new RequestData
            {
                companyId = 103136,
                password = "LogMark2023", ///"7nq6hxRu&rw0#LyJ",
                documentNumber = "1000002", //NIT EMPRESA
                documentType = "Q",
                documentExtension = "SN",
                documentComplement = "",
                amount = model.monto, //MONTO
                currency = "BOL", //MONEDA
                fundSource = "GIRO COMERCIAL POR DISTRIBUCION DE PRODUCTOS MASIVOS",
                fundDestination = model.concepto,  //DESTINO DE FONDOS
                sourceAccount = "2015046782327", //CUENTA SALIDA
                sourceCurrency = "BOL", //MONEDA CUENTA
                cismartApprovers = aprobadores,
                spreadsheet = new Spreadsheet
                {
                    formOddPayments = new List<FormOddPayment>(),
                    formProvidersPayments = pagoBCP,
                    formAchPayments = pagoACH
                }
            };

            // Serializar el objeto RequestData a una cadena JSON
            string jsonDatabanco = JsonConvert.SerializeObject(requestDatas);
            //Encriptar la cadena JSON
            string jsonDataEncrip = Encrip.Encrypt(jsonDatabanco);
            //Enviar informacion al destino mediante una tarea asincrona,
            //con los datos proporcionados por el banco
            string response = await PostDataAsyncLog(jsonDataEncrip);
            //Desencriptar la respuesta del banco
            string responseDecryp = Encrip.Decrypt(response);
            // Deserializar la respuesta y dividir
            ApiResponse responseJson = JsonConvert.DeserializeObject<ApiResponse>(responseDecryp);

            string result = responseJson.Result;
            string code = responseJson.Code;
            string message = responseJson.Message;
            //verificar que la respuesta no sea nula
            if (response == null)
            {
                return BadRequest();
            }
            //Enviar la respuesta al Front End 
            return Ok(new { lote = result, code = code, message = message });
        }

        private static async Task<string> PostDataAsyncLog(string jsonDataBanco)
        {
            string url = "https://credinetweb.bcp.com.bo/ApiGatewayCw/APIAuthentication/api/APIAuthentication/PreparerBatch";

            using (var httpClientHandler = new HttpClientHandler())
            {

                string username = "USRCWLOGMA";
                string password = "j96hs&YUmy0rB!ix";
                string companyID = "103136";

                string currentDirectory = Directory.GetCurrentDirectory();

                // Construir la ruta completa al certificado PFX
                string certPath = Path.Combine(currentDirectory, "cert", "BCPCW_API_EMPRESA2.pfx");


                var requestData = new
                {
                    companyId = companyID, // Código de compañía proporcionado por el banco
                    data = jsonDataBanco   // Información encriptada
                };

                // Serializar el objeto JSON a una cadena
                string jsonData = JsonConvert.SerializeObject(requestData);

                // Cargar el certificado PFX desde el archivo
                X509Certificate2 certificate = new X509Certificate2(certPath, "C3pdcvtJ1R7a1,my");

                // Agregar el certificado a la solicitud HTTP para autenticación
                httpClientHandler.ClientCertificates.Add(certificate);


                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    // Codificar las credenciales en base64
                    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                    // Agregar el encabezado Authorization a la solicitud
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                    // Agregar otros encabezados si es necesario
                    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    httpClient.DefaultRequestHeaders.Add("Correlation-Id", "123");

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await httpClient.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();

                        return await response.Content.ReadAsStringAsync();
                    }
                    catch (HttpRequestException e)
                    {
                        // Manejar el error si la solicitud no es exitosa
                        //Console.WriteLine($"Error: {e.Message}");
                        return null;
                    }
                }
            }
        }






        private bool OrdendepagoExists(int id)
        {
            return _context.Ordendepagos.Any(e => e.idordendepago == id);
        }


        public class ApiResponse
        {
            public string Result { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
        }

        public class CismartApprover
        {
            public string idc { get; set; }
            public int type { get; set; }
        }

        public class FormProvidersPayment
        {
            public string paymentType { get; set; }
            public int line { get; set; }
            public string accountNumber { get; set; }
            public string glossPayment { get; set; }
            public double amount { get; set; }
            public string documentType { get; set; }
            public string documentNumber { get; set; }
            public string documentExtension { get; set; }
            public string firstDetail { get; set; }
            public string secondDetail { get; set; }
            public string mail { get; set; }
        }

        public class FormAchPayment
        {
            public string paymentType { get; set; }
            public int line { get; set; }
            public string accountNumber { get; set; }
            public string titularName { get; set; }
            public string firstLastName { get; set; }
            public string secondLastName { get; set; }
            public double amount { get; set; }
            public int branchOfficeId { get; set; }
            public string firstDetail { get; set; }
            public string mail { get; set; }
             public int bankId { get; set; }
           
        }

        public class FormOddPayment
        {
            public string paymentType { get; set; }
            public int line { get; set; }
            public string accountNumber { get; set; }
            public string titularName { get; set; }
            public string firstLastName { get; set; }
            public string secondLastName { get; set; }
            public string description { get; set; }
            public string glossPayment { get; set; }
            public double amount { get; set; }
            public string documentType { get; set; }
            public string documentNumber { get; set; }
            public string documentExtension { get; set; }
            public int branchOfficeId { get; set; }
            public string firstDetail { get; set; }
            public string secondDetail { get; set; }
            public string mail { get; set; }
            //public string bankId { get; set; }
            public double commission { get; set; }
            public string CommissionCurrency { get; set; }
        }

        public class Spreadsheet
        {
            public List<FormProvidersPayment> formProvidersPayments { get; set; }
            public List<FormAchPayment> formAchPayments { get; set; }
            public List<FormOddPayment> formOddPayments { get; set; }
        }

        public class RequestData
        {
            public int companyId { get; set; }
            public string password { get; set; }
            public string documentNumber { get; set; }
            public string documentType { get; set; }
            public string documentExtension { get; set; }
            public string documentComplement { get; set; }
            public double amount { get; set; }
            public string currency { get; set; }
            public string fundSource { get; set; }
            public string fundDestination { get; set; }
            public string sourceAccount { get; set; }
            public string sourceCurrency { get; set; }
            public string description { get; set; }
            public string sendVouchers { get; set; }
            public List<CismartApprover> cismartApprovers { get; set; }
            public Spreadsheet spreadsheet { get; set; }
        }

    }
}
