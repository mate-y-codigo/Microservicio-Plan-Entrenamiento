using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Commands
{
    public class TrainingSessionCommand : ITrainingSessionCommand
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public TrainingSessionCommand(ConfigRutinaDB configRutinaDB) { 
        
            _configRutinaDB = configRutinaDB;
        }

        public async Task insertTrainingSession(SesionEntrenamiento TS)
        {
            _configRutinaDB.Add(TS);
            await _configRutinaDB.SaveChangesAsync();
        }

        public async Task updateTrainingSession(SesionEntrenamiento TS)
        {
            await _configRutinaDB.SesionEntrenamientos.Where(ts => ts.Id == TS.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(ts => ts.Nombre, TS.Nombre)
                .SetProperty(ts => ts.Orden, TS.Orden)
                .SetProperty(ts => ts.IdPlanEntrenamiento, TS.IdPlanEntrenamiento)
                );
        }
    }
}
