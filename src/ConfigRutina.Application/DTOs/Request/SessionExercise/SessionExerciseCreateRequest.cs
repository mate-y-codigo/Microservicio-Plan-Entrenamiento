using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.SessionExercise
{
    public class SessionExerciseCreateRequest
    {
        public required Guid exerciseId { get; set; }
        public required int targetSets { get; set; }
        public required int targetReps { get; set; }
        public float targetWeight { get; set; }
        public required int rest { get; set; }
        public required int order { get; set; }
    }
}
