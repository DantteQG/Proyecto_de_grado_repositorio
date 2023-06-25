using System;

namespace Sistema.Web.Models.Solicitud.Ordendepago
{
    public class PivotViewModel
    {
        public decimal total { get; set; }
        public string fechapromada { get; set; }

        public int idtipogasto { get; set; }
        public string nombretipogasto { get; set; }
        public int idespecifgasto { get; set; }
        public string especifgasto { get; set; }

    }
}
