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
        public required string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public required bool isTemplate { get; set; } = false;
        public required Guid trainerId { get; set; }
        public List<TrainingSessionCreateRequest> TrainingSessionCreateRequests { get; set; } = new();
    }
}
