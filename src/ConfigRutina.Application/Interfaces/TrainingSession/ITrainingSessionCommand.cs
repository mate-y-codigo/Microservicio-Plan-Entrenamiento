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
        Task insertTrainingSession(SesionEntrenamiento TS);
        Task updateTrainingSession(SesionEntrenamiento TS);
    }
}
