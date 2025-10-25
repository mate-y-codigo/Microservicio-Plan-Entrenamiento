using ConfigRutina.Application.Enums;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingPlan
{
    public interface ITrainingPlanQuery
    {
        Task<PlanEntrenamiento> GetTrainingPlanById(Guid id);
        Task<List<PlanEntrenamiento>> GetTrainingPlanFilter(string? name, bool? isTemplate, Guid? trainerId, bool? active, DateTime? from, DateTime? to, TrainingPlanOrderBy orderBy);
    }
}
