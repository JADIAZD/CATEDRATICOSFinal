using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalDesarrollo.DbModels;
using FinalDesarrollo.Models;

namespace FinalDesarrollo.Controllers.Api
{
    [ApiController]
    [Produces("application/json")]
    public class NotasController : ControllerBase
    {
        private readonly ctrlCatedraticosContext _context;

        public NotasController(ctrlCatedraticosContext context)
        {
            _context = context;
        }

        // PUT: api/Notas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("api/Notas/ActualizarNota/{id}")]
        public async Task<IActionResult> PutAsignacion(int id, NotasRequest asignacionRequest)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var asignacion = _context.Asignacioncurso.First(x => x.AsignacioncursoId == id);
            asignacion.CatedraticoId = asignacionRequest.Catedratico;
            asignacion.CursoId = asignacionRequest.CursoId;
            asignacion.Notaalumnos = asignacionRequest.Notaalumnos;
            asignacion.Zonaalumnos = asignacionRequest.Zonaalumnos;
            asignacion.ExFinal = asignacionRequest.ExamenFinal;

            _context.Asignacioncurso.Update(asignacion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("api/Notas/IngresarNota")]
        public async Task<ActionResult<NotasRequest>> PostAsignacion(NotasRequest asignacioncurso)
        {
            var newAsignacion = new Asignacion
            {
                Catedratico = _context.Catedratico.First(x => x.CatedraticoId == asignacioncurso.Catedratico),
                CatedraticoId = asignacioncurso.Catedratico,
                Curso = _context.Cursos.First(x => x.CursoId == asignacioncurso.CursoId),
                CursoId = asignacioncurso.CursoId,
                Notaalumnos = asignacioncurso.Notaalumnos,
                Zonaalumnos = asignacioncurso.Zonaalumnos,
                ExFinal = asignacioncurso.ExamenFinal
            };

            _context.Asignacioncurso.Add(newAsignacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion", new { id = newAsignacion.AsignacioncursoId }, asignacioncurso);
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignacioncurso.Any(e => e.AsignacioncursoId == id);
        }
    }
}
