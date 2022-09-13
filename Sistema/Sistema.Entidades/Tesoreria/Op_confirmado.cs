using Sistema.Entidades.Solicitud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Tesoreria
{
    public class Op_confirmado
    {
        public int idconfirmado { get; set; }
        [Required]
        [ForeignKey("ordendepago")]
        public int idordendepago { get; set; }
        public DateTime fecha { get; set; }

        public Ordendepago ordendepago { get; set; }
    }
}
