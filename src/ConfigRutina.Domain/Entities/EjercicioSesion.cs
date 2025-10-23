using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class EjercicioSesion
    {
        public Guid Id { get; set; }
        public int SeriesObjetivo { get; set; }
        public int RepeticionesObjetivo { get; set; }
        public float PesoObjetivo { get; set; }
        public int Descanso { get; set; }
        public int Orden { get; set; }

        // FK
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdEjercicio { get; set; }

        // Relacion
        public SesionEntrenamiento? SesionEntrenamientoEn { get; set; }
        public Ejercicio? EjercicioEn { get; set; }
    }
}
