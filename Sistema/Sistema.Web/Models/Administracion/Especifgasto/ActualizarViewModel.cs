﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Especifgasto
{
    public class ActualizarViewModel
    {
       
        [Required]
        public int idespecifgasto { get; set; }
        [Required]
        public int idtipogasto { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int dias { get; set; }

    }
}
