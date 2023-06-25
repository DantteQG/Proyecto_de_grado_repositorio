using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Tesoreria;
using Sistema.Web.Models.Credinet.Cargabanco;
using Sistema.Web.Models.Tesoreria.Op_cargado;

using System.Text.Json;
using Sistema.Web.Models.Credinet.Aprobador;
using Sistema.Web.Models.Credinet.TipoOperacion;


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Op_cargadosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public Op_cargadosController(DbContextSistema context)
        {
            _context = context;
        }

        // POST: api/Op_cargados/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = DateTime.Now;

            Op_cargado op_cargado = new Op_cargado
            {
                idordendepago = model.idordendepago,
                idcuentasalida = model.idcuentasalida,
                lote = model.lote,
                fecha = fecha
                
            };

            _context.Op_cargados.Add(op_cargado);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();

        }

        // GET: api/Op_cargados/Listar/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<Op_cargadoViewModel>> Listar([FromRoute] int id)
        {
            var opcargadp = await _context.Op_cargados
                .Where(opcarg => opcarg.idordendepago == id).ToListAsync();


            return opcargadp.Select(opcarg => new Op_cargadoViewModel
            {
                idcuentasalida = opcarg.idcuentasalida,
                lote = opcarg.lote

            });
        }
        // POST: api/Op_cargados/CargaApi
        [HttpPost("[action]")]
        //public async Task<ActionResult> Cargarapi([FromBody]Cargabanco model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //model.aprobador ={ aprobador}

        //    AprobadorVM aprobador = new AprobadorVM()
        //    {
        //        idc = "cmonje",
        //        type = 1
                
        //    };

        //    //if PROV O ACH
        //    TipoOperacionVM tipooperacion2 = new TipoOperacionVM
        //    {
        //        paymentType = "fasdfasdf",
        //        line = 1,
        //    };

        //    //Cargabanco cargabanco = new Cargabanco
        //    //{

        //    //    companyId = 002,
        //    //    password = "pato123",
        //    //    documentNumber = "7042665",
        //    //    documentType = "Q",
        //    //    documentExtension = "LP",
        //    //    documentComplement = "",
        //    //    amount = model.amount,
        //    //    currency = model.currency,
        //    //    fundSource = "Giro comercial por distribucion de productos masivos",
        //    //    fundDestination = model.fundDestination,//destino de fondos
        //    //    sourceAccount = model.sourceAccount,//cuenta destino
        //    //    sourceCurrency = model.sourceCurrency,
        //    //    description = model.description,
        //    //    sendVouchers = model.sendVouchers,
        //    //    aprobador = new List<AprobadorVM>(),
        //    //    aprobador = new AprobadorVM()
        //    //    { idc =["cmonje", "dqg"], 
        //    //          type = [1,2]
        //    //        },
        //    //    tipooperacion = tipooperacion2[],





        //    //};

            
        //    cargabanco.tipooperacion = list<tipooperacion2>;

        //    string jsonstring = JsonSerializer.Serialize(cargabanco);


            
        //    string jsonencriptado = Encrip.Encrypt(jsonstring);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();

        //}


        private bool Op_cargadoExists(int id)
        {
            return _context.Op_cargados.Any(e => e.idcargado == id);
        }
    }
}
