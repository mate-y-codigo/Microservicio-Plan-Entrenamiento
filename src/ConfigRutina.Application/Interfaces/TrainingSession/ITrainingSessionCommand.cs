using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingSession
{
    public interface ITrainingSessionCommand
    {
        Task InsertTrainingSession(SesionEntrenamiento TS);
        Task UpdateTrainingSession(SesionEntrenamiento TS);
    }
}
