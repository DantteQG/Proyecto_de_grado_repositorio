using Sistema.Web.Models.Credinet.TipoOperacion;
using Sistema.Web.Models.Credinet.Aprobador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Credinet.Cargabanco
{
    public class Cargabanco
    {
        public long companyId { get; set; }
        public string password { get; set; }
        public string documentNumber { get; set; }
        public string documentType { get; set; }
        public string documentExtension { get; set; }
        public string documentComplement { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string fundSource { get; set; }
        public string fundDestination { get; set; }
        public string sourceAccount { get; set; }
        public string sourceCurrency { get; set; }
        public string description { get; set; }
        public string sendVouchers { get; set; }

        
        public List<TipoOperacionVM> tipooperacion { get; set; }
        
        public List<AprobadorVM> aprobador { get; set; }

    }
}
