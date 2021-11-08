using System;
using System.Collections.Generic;

#nullable disable

namespace FinalDesarrollo.DbModels
{
    public partial class Catedraticos
    {
        public Catedraticos()
        {
            Asignacions = new HashSet<Asignacion>();
        }

        public int CatedraticoId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Codigo { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Asignacion> Asignacions { get; set; }
    }
}
