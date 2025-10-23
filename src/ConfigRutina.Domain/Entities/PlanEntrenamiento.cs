using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class PlanEntrenamiento
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool EsPlantilla { get; set; } = false;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Activo { get; set; } = true;

        // FK
        public Guid IdEntrenador { get; set; }

        // relacion
        public ICollection<SesionEntrenamiento>? SesionEntrenamientoLista { get; set; }
    }
}
