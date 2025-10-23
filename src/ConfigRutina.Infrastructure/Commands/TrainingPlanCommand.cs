using ConfigRutina.Application.Interfaces.TrainingPlan;
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
    public class TrainingPlanCommand : ITrainingPlanCommand
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public TrainingPlanCommand(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task InsertTrainingPlan(PlanEntrenamiento TP)
        {
            _configRutinaDB.Add(TP);
            await _configRutinaDB.SaveChangesAsync();
        }

        public async Task UpdateTrainingPlan(PlanEntrenamiento TP)
        {
            await _configRutinaDB.PlanEntrenamientos
                .Where(pe => pe.Id == TP.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(pe => pe.Nombre, TP.Nombre)
                .SetProperty(pe => pe.Descripcion, TP.Descripcion)
                .SetProperty(pe => pe.EsPlantilla, TP.EsPlantilla)
                .SetProperty(pe => pe.Activo, TP.Activo)
                .SetProperty(pe => pe.FechaActualizacion, TP.FechaActualizacion)
                );
            await _configRutinaDB .SaveChangesAsync();
        }
    }
}
