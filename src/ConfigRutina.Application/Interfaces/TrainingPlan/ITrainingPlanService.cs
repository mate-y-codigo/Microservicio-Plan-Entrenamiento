using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingPlan
{
    public interface ITrainingPlanService
    {
        public TrainingPlanResponse CreateTrainingPLan(CreateTrainingPlanRequest request);
        public TrainingPlanResponse UpdateTrainingPlan(); // preguntar si esta bien
        public TrainingPlanResponse GetTrainingPlanById(string id);
        public TrainingPlanStatusResponse ChangeStateTrainingPlan(string id, UpdateTrainingPlanStatusRequest request);
        public List<TrainingPlanResponse> GetFilterTrainingPlan(string? name, bool? plantilla, string? IdEntrenador, bool? active, DateTime? CreateDate,DateTime? UpdateDate);
    }
}
