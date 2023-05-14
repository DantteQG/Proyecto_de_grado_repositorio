using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Credinet.TipoOperacion
{
    public class TipoOperacionVM
    {
        public string paymentType { get; set; } //String(3) Tipo de Operacion: PROV, ACH, ODD
        public int line { get; set; } //int Línea de la operación.Ejemplo: 1,2,3...
        public string accountNumber { get; set; } // String(26) Número de Cuenta Destino
        public string glossPayment { get; set; } //String(100) Descripción de la glosa de la transacción
        public decimal amount { get; set; } //decimal Monto de la transacción
        public string documentType { get; set; } //String(1) Tipo de documento
        public string documentNumber { get; set; } //String(12) Número de documento
        public string documentExtension { get; set; } //String(3) Extensión de documento
        public string firstDetail { get; set; } //String(100) Descripción de detalle 1 (Opcional)
        public string secondDetail { get; set; } //String(100) Descripción de detalle 2 (Opcional)
        public string mail { get; set; } //String(100) Correo para envió de comprobante(Opcional)
        public string titularName { get; set; } //String(100) Nombre del titular de cuenta
        public string firstLastName { get; set; } //String(100) Apellido paterno
        public string secondLastName { get; set; } //String(100) Apellido materno(opcional)
        public int branchOfficeId { get; set; } //Int Código de Sucursal: 201,...
        public string bankId { get; set; } //String(15) Código de banco: 1014,...
        public decimal commission { get; set; } //decimal Monto de comisión(Opcional)
        public string CommissionCurrency { get; set; } //String(3) Moneda de comisión(Opcional)


        //public class FormAchPayment
        //{
        //    public string paymentType { get; set; }
        //    public int line { get; set; }
        //    public string accountNumber { get; set; }
        //    public string titularName { get; set; }
        //    public string firstLastName { get; set; }
        //    public string secondLastName { get; set; }
        //    public double amount { get; set; }
        //    public int branchOfficeId { get; set; }
        //    public string firstDetail { get; set; }
        //    public string mail { get; set; }
        //    public string bankId { get; set; }
        //}

        //public class FormProvidersPayment
        //{
        //    public string paymentType { get; set; }
        //    public int line { get; set; }
        //    public string accountNumber { get; set; }
        //    public string glossPayment { get; set; }
        //    public double amount { get; set; }
        //    public string documentType { get; set; }
        //    public string documentNumber { get; set; }
        //    public string documentExtension { get; set; }
        //    public string firstDetail { get; set; }
        //    public string secondDetail { get; set; }
        //    public string mail { get; set; }
        //}

        //public class Root
        //{
        //    public Spreadsheet spreadsheet { get; set; }
        //}

        //public class Spreadsheet
        //{
        //    public List<FormProvidersPayment> formProvidersPayments { get; set; }
        //    public List<FormAchPayment> formAchPayments { get; set; }
        //}

    }
}
