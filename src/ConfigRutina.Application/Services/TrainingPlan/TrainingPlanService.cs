using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.TrainingPlan
{
    public class TrainingPlanService : ITrainingPlanService
    {
        public TrainingPlanStatusResponse ChangeStateTrainingPlan(string id,UpdateTrainingPlanStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public TrainingPlanResponse CreateTrainingPLan(CreateTrainingPlanRequest request)
        {
            throw new NotImplementedException();
        }

        public List<TrainingPlanResponse> GetFilterTrainingPlan(string? name, bool? plantilla, string? IdEntrenador, bool? active, DateTime? CreateDate, DateTime? UpdateDate)
        {
            throw new NotImplementedException();
        }

        public TrainingPlanResponse GetTrainingPlanById(string id)
        {
            throw new NotImplementedException();
        }

        public TrainingPlanResponse UpdateTrainingPlan()
        {
            throw new NotImplementedException();
        }
    }
}
