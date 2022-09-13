using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Tesoreria;
using Sistema.Web.Models.Tesoreria.Op_confirmado;

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

        // POST: api/Op_confirmados/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = DateTime.Now;

            Op_confirmado op_confirmado = new Op_confirmado
            {
                idordendepago = model.idordendepago,
                fecha = fecha
            };

            _context.Op_confirmados.Add(op_confirmado);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();

        }

        private bool Op_confirmadoExists(int id)
        {
            return _context.op_Confirmados.Any(e => e.idconfirmado == id);
        }
    }
}
