using ConfigRutina.Application.DTOs.Request.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.TrainingPlan
{
    public class CreateTrainingPlanRequest
    {
        public required string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public required bool esPlantilla { get; set; } = false;
        public required Guid idEntrenador { get; set; }
        public List<TrainingSessionCreateRequest> TrainingSessionCreateRequests { get; set; } = new();
    }
}
