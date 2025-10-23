using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class SesionEntrenamiento
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Orden { get; set; }

        // FK
        public Guid IdPlanEntrenamiento { get; set; }

        // relacion
        public PlanEntrenamiento? PlanEntrenamientoEn { get; set; }
        public ICollection<EjercicioSesion>? EjercicioSesionLista { get; set; }
    }
}
