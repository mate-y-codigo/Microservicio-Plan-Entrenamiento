using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingSession
{
    public interface ITrainingSessionQuery
    {
        Task<SesionEntrenamiento> GetById(Guid id);
        Task<List<SesionEntrenamiento>> GetTrainingSessionsByPlan(Guid idTP);
    }
}
