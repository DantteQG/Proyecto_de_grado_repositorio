using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Tesoreria.Op_cargado
{
    public class CrearViewModel
    {
        
        public int idordendepago { get; set; }
        public int idcuentasalida { get; set; }
        public string lote { get; set; }
        public DateTime fecha { get; set; }
    }
}
