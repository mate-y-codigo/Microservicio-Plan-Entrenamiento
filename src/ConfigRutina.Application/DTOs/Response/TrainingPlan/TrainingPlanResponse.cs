using ConfigRutina.Application.DTOs.Response.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.TrainingPlan
{
    public class TrainingPlanResponse
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public bool isTemplate { get; set; }
        public Guid trainerId { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool active { get; set; } = false;
        public List<TrainingSessionResponse> trainingSessions { get; set; } = new();
    }
}
