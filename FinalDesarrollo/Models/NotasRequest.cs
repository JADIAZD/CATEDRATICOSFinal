using System;
namespace FinalDesarrollo.Models
{
    public class NotasRequest
    {
        public int Catedratico { get; set; }
        public int CursoId { get; set; }
        public decimal Notaalumnos { get; set; }
        public decimal Zonaalumnos { get; set; }
        public decimal ExamenFinal { get; set; }
    }
}
