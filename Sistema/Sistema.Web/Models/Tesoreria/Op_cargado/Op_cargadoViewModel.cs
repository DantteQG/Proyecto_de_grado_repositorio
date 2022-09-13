using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Tesoreria.Op_cargado
{
    public class Op_cargadoViewModel
    {
        public string idcuentasalida { get; set; }
        public string cuentasalida { get; set; }
        public string nombrecuenta { get; set; }
        public string lote { get; set; }
        public DateTime fecha { get; set; }
    }
}
