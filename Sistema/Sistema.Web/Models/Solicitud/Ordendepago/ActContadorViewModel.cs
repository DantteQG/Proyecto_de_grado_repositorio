using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Solicitud.Ordendepago
{
    public class ActContadorViewModel
    {
        [Required]
        public int idordendepago { get; set; }
        [Required]
        public int idcontador { get; set; }
    }
}
