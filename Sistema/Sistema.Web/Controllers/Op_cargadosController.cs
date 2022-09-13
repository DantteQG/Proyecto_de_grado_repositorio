using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Tesoreria;
using Sistema.Web.Models.Tesoreria.Op_cargado;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Op_cargadosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public Op_cargadosController(DbContextSistema context)
        {
            _context = context;
        }

        // POST: api/Cuentasalidas/Crear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime fecha = DateTime.Now;

            Op_cargado op_cargado = new Op_cargado
            {
                idordendepago = model.idordendepago,
                idcuentasalida = model.idcuentasalida,
                lote = model.lote,
                fecha = fecha
                
            };

            _context.Op_cargados.Add(op_cargado);

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

        private bool Op_cargadoExists(int id)
        {
            return _context.Op_cargados.Any(e => e.idcargado == id);
        }
    }
}
