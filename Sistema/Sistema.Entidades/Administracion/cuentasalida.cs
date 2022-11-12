using Sistema.Entidades.Tesoreria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Administracion
{
    public class Cuentasalida
    {
        public int idcuentasalida { get; set; }
        [Required]
        [ForeignKey("banco")]
        public int idempresa { get; set; }
        [Required]
        [ForeignKey("banco")]
        public int idbanco { get; set; }
        [Required]
        [ForeignKey("moneda")]
        public int idmoneda { get; set; }
        public string cuenta { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }


        public Banco banco { get; set; }
        public Moneda moneda { get; set; }
        public Empresa empresa { get; set; }



        public ICollection<Op_cargado> op_cargado { get; set; }
    }
}
