using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingPlan
{
    public interface ITrainingPlanCommand
    {
        Task InsertTrainingPlan(PlanEntrenamiento TP);
        Task UpdateTrainingPlan(PlanEntrenamiento TP);
    }
}
