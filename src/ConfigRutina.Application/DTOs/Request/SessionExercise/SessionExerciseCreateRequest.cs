using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.SessionExercise
{
    public class SessionExerciseCreateRequest
    {
        public required Guid id { get; set; }
        public required int seriesObjetivo { get; set; }
        public required int repeticionesObjetivo { get; set; }
        public float pesoObjetivo { get; set; }
        public required int descanso { get; set; }
        public required int orden { get; set; }
    }
}
