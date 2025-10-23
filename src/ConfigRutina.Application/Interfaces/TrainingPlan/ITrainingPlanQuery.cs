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
        Task<List<PlanEntrenamiento>> GetTrainingPLanFilter(DateTime? createDate, DateTime? updateDate, Guid? idEntrenador, string? name, bool plantilla = true, bool active = true);

    }
}
