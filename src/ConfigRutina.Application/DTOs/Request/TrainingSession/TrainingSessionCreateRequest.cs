using ConfigRutina.Application.DTOs.Request.SessionExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.TrainingSession
{
    public class TrainingSessionCreateRequest
    {
        public required string nombre { get; set; } = string.Empty;
        public required int orden { get; set; }
        public List<SessionExerciseCreateRequest> sessionExerciseCreateRequests { get; set; } = new();
    }
}
