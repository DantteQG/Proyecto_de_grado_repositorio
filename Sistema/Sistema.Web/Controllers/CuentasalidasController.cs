using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Administracion;
using Sistema.Web.Models.Administracion.Cuentasalida;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasalidasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CuentasalidasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Cuentasalidas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CuentasalidaViewModel>> Listar()
        {
            var cuentasalida = await _context.Cuentasalidas
                .Include(cs => cs.empresa)
                .Include(cs => cs.banco)
                .Include(cs => cs.moneda).ToListAsync();

            return cuentasalida.Select(cs => new CuentasalidaViewModel
            {
                idcuentasalida = cs.idcuentasalida,
                idempresa = cs.idempresa,
                empresa = cs.empresa.nombre,
                idbanco = cs.idbanco,
                banco = cs.banco.alias,
                idmoneda = cs.idmoneda,
                moneda = cs.moneda.nombre,
                cuenta = cs.cuenta,
                descripcion = cs.descripcion,
                condicion = cs.condicion
            });
        }

        // GET: api/Cuentasalidas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var cuentasalida = await _context.Cuentasalidas
                .Include(cs => cs.empresa)
                .Include(cs => cs.banco)
                .Include(cs => cs.moneda)
                .Where(cs => cs.condicion == true).ToListAsync();

            return cuentasalida.Select(cs => new SelectViewModel
            {
                idcuentasalida = cs.idcuentasalida,
                empresa = cs.empresa.nombre,
                banco = cs.banco.alias,
                moneda = cs.moneda.nombre,
                cuenta = cs.cuenta
            });
        }


        // PUT: api/Cuentasalidas/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcuentasalida <= 0)
            {
                return BadRequest();
            }

            var cuentasalida = await _context.Cuentasalidas.FirstOrDefaultAsync(c => c.idcuentasalida == model.idcuentasalida);


            if (cuentasalida == null)
            {
                return NotFound();
            }
            cuentasalida.idempresa = model.idempresa;
            cuentasalida.idbanco = model.idbanco;
            cuentasalida.idmoneda = model.idmoneda;
            cuentasalida.cuenta = model.cuenta;
            cuentasalida.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
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

            Cuentasalida cuentasalida = new Cuentasalida
            {
                idempresa = model.idempresa,
                idbanco = model.idbanco,
                idmoneda = model.idmoneda,
                cuenta = model.cuenta,
                descripcion = model.descripcion,
                condicion = true

            };

            _context.Cuentasalidas.Add(cuentasalida);

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

        // PUT: api/Cuentasalidas/Desactivar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var cuentasalida = await _context.Cuentasalidas.FirstOrDefaultAsync(c => c.idcuentasalida == id);


            if (cuentasalida == null)
            {
                return NotFound();
            }

            cuentasalida.condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Cuentasalidas/Activar/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var cuentasalida = await _context.Cuentasalidas.FirstOrDefaultAsync(c => c.idcuentasalida == id);


            if (cuentasalida == null)
            {
                return NotFound();
            }

            cuentasalida.condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        private bool CuentasalidaExists(int id)
        {
            return _context.Cuentasalidas.Any(e => e.idcuentasalida == id);
        }
    }
}
