﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Administracion.Banco
{
    public class ActualizarViewModel
    {
        public int idbanco { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        public string alias { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }

        [Required]
        public int codigobanco { get; set; }

        public bool cuentanacional { get; set; }
    }
}
