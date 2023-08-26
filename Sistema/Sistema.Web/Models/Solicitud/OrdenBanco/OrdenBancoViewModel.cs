using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Solicitud.OrdenBanco
{
    public class OrdenBancoViewModel
    {
        public int tipopago { get; set; }
        public string nit { get; set; }
        public double monto { get; set; }
        public string moneda { get; set; }
        public string concepto { get; set; } //destino de fondos y descripcion
        public string cuentasalida { get; set; }
        public string monedasalida { get; set; }
        public string emailboucher { get; set; }
        public string nrocuenta { get; set; }
        public string nrodecaso { get; set; }
        public string codigobanco { get; set; }
        
        public string nombretitular { get; set; }

        public int idbancodestino { get; set; }




    }
}
