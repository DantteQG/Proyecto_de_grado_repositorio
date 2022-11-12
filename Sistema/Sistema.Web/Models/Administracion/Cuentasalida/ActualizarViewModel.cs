using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Cuentasalida
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcuentasalida { get; set; }
        [Required]
        public int idempresa { get; set; }
        [Required]
        public int idbanco { get; set; }
        [Required]
        public int idmoneda { get; set; }

        [Required]
        public string cuenta { get; set; }
        public string descripcion { get; set; }

    }
}
