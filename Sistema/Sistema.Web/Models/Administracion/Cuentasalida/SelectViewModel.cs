using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuentasalida
{
    public class SelectViewModel
    {
        public int idcuentasalida { get; set; }
        public string banco { get; set; }
        public string moneda { get; set; }
        public string cuenta { get; set; }

    }
}
