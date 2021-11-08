using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FinalDesarrollo.DbModels
{
    public partial class Asignacion
    {
        public int AsignacioncursoId { get; set; }
        public int CatedraticoId { get; set; }
        public int CursoId { get; set; }
        public decimal? Notaalumnos { get; set; }
        public decimal? Zonaalumnos { get; set; }
        public decimal? ExFinal { get; set; }

        public virtual Catedraticos Catedratico { get; set; }
        public virtual Curso Curso { get; set; }

        [NotMapped]
        public decimal NotaFinal { get { return (Notaalumnos ?? 0) + (Zonaalumnos ?? 0) + (ExFinal ?? 0); } }
    }
}
