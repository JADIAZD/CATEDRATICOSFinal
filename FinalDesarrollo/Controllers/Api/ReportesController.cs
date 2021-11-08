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
    public class ReportesController : ControllerBase
    {
        private readonly ctrlCatedraticosContext _context;

        public ReportesController(ctrlCatedraticosContext context)
        {
            _context = context;
        }

        // GET: api/Catedratico
        [HttpGet]
        [Route("api/Reportes/ObtenerCatedraticoActivos")]
        public async Task<ActionResult<IEnumerable<Catedraticos>>> Getcatedraticos()
        {
            return await _context.Catedratico.Where(x => x.Activo).ToListAsync();
        }

        [HttpGet]
        [Route("api/Reportes/ObtenerCatedraticosAprobados")]
        public IEnumerable<ReporteNotas> GetCatedraticosAprobados()
        {
            var test = (from nota in _context.Asignacioncurso
                        join Catedraticos in _context.Catedratico on nota.CatedraticoId equals Catedraticos.CatedraticoId
                        where (nota.Notaalumnos + nota.Zonaalumnos + nota.ExFinal) >= 61
                        select new ReporteNotas
                        {
                            Codigo = Catedraticos.Codigo,
                            Nombre = Catedraticos.Nombre,
                            Nota = nota.Notaalumnos ?? 0,
                            Zona = nota.Zonaalumnos ?? 0,
                            ExamenFinal = nota.ExFinal ?? 0,
                            Total = (nota.Notaalumnos + nota.Zonaalumnos + nota.ExFinal) ?? 0
                        }).ToList();

            return test;
        }

        [HttpGet]
        [Route("api/Reportes/ObtenerCatedraticosReprobados")]
        public IEnumerable<ReporteNotas> GetCatedraticosReprobados()
        {
            var test = (from nota in _context.Asignacioncurso
                        join Catedraticos in _context.Catedratico on nota.CatedraticoId equals Catedraticos.CatedraticoId
                        where (nota.Notaalumnos + nota.Zonaalumnos + nota.ExFinal) < 61
                        select new ReporteNotas
                        {
                            Codigo = Catedraticos.Codigo,
                            Nombre = Catedraticos.Nombre,
                            Nota = nota.Notaalumnos ?? 0,
                            Zona = nota.Zonaalumnos ?? 0,
                            ExamenFinal = nota.ExFinal ?? 0,
                            Total = (nota.Notaalumnos + nota.Zonaalumnos + nota.ExFinal) ?? 0
                        }).ToList();

            return test;
        }
    }
}