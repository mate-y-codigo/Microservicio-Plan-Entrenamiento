using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.ExerciseSession
{
    public class ExerciseSessionShortResponse
    {
        public Guid id { get; set; }
        public Guid idEjercicio { get; set; }
        public string nombreEjercicio { get; set; } = string.Empty;
        public int seriesObjetivo { get; set; }
        public int repeticionesObjetivo { get; set; }
        public float pesoObjetivo { get; set; }
        public int descanso { get; set; }
    }
}
