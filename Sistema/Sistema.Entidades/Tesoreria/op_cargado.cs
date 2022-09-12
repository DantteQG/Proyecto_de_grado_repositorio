using Sistema.Entidades.Administracion;
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
    public class Op_cargado
    {
        public int idcargado { get; set; }
        [Required]
        [ForeignKey("ordendepago")]
        public int idordendepago { get; set; }
        [Required]
        [ForeignKey("cuentasalida")]
        public int idcuentasalida { get; set; }
        public string lote { get; set; }
        public DateTime fecha { get; set; }

        public Ordendepago ordendepago { get; set; }
        public Cuentasalida cuentasalida { get; set; }
       

        
    }
}
