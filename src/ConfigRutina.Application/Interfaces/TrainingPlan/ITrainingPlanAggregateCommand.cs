using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingPlan
{
    public interface ITrainingPlanAggregateCommand
    {
        Task CreatePlanCompleteAsync(PlanEntrenamiento plan, List<SesionEntrenamiento> sessions, List<EjercicioSesion> exercises);
    }

}
