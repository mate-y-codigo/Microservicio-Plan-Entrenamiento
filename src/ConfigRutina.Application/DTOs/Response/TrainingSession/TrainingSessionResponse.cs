using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.TrainingSession
{
    public class TrainingSessionResponse
    {
        public Guid id { get; set; }
        public Guid planEntrenamientoId { get; set; }
        public string nombre { get; set; } = string.Empty;
        public int orden { get; set; }
        public List<ExerciseSessionShortResponse> sesionesEjercicio { get; set; } = new();
    }
}
