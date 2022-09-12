using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Tesoreria
{
    public class op_cargado
    {
        public int idcargado { get; set; }
        [Required]
        [ForeignKey("ordendepago")]
        public int idordendepago { get; set; }
        [Required]
        [ForeignKey("cuentasalida")]
        public int idcuentasalid { get; set; }
        public string lote { get; set; }
        public DateTime fecha { get; set; }

        //public ICollection<Ordendepago> ordendepago { get; set; }
    }
}
