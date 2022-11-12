using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuentasalida
{
    public class CuentasalidaViewModel
    {
        public int idcuentasalida { get; set; }
        public int idempresa { get; set; }
        public string empresa { get; set; }
        public int idbanco { get; set; }
        public string banco { get; set; }
        public int idmoneda { get; set; }
        public string moneda { get; set; }
        public string cuenta { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

    }
}
