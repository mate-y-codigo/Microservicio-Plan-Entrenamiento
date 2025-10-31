using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.Enums;
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
        Task<TrainingPlanResponse> CreateTrainingPlan(CreateTrainingPlanRequest request);
        public TrainingPlanResponse UpdateTrainingPlan();
        Task<TrainingPlanResponse> GetTrainingPlanById(Guid id);
        public TrainingPlanStatusResponse ChangeStateTrainingPlan(string id, UpdateTrainingPlanStatusRequest request);
        Task<List<TrainingPlanResponse>> GetFilterTrainingPlan(string? name, bool? plantilla, Guid? IdEntrenador, bool? active, DateTime? from, DateTime? to, TrainingPlanOrderBy OrderBy);
        Task<TrainingPlanStatusResponse> SetStatus(string? strId, bool status);
    }
}
