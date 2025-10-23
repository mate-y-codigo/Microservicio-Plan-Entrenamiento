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
        public Guid trainingPlanId { get; set; }
        public string name { get; set; } = string.Empty;
        public int order { get; set; }
        public List<ExerciseSessionShortResponse> exerciseSession {get;set;}
         
    }
}
