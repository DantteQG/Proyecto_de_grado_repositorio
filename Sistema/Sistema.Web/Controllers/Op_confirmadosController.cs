using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Tesoreria;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Op_confirmadosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public Op_confirmadosController(DbContextSistema context)
        {
            _context = context;
        }

        

        private bool Op_confirmadoExists(int id)
        {
            return _context.op_Confirmados.Any(e => e.idconfirmado == id);
        }
    }
}
