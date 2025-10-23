using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Queries
{
    public class TrainingSessionQuery : ITrainingSessionQuery
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public TrainingSessionQuery(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task<SesionEntrenamiento> GetById(Guid id)
        {
            return _configRutinaDB.SesionEntrenamientos
                .AsNoTracking()
                .Include(ts => ts.PlanEntrenamientoEn)
                .Where(ts => ts.Id == id)
                .FirstOrDefault();
        }

        public async Task<List<SesionEntrenamiento>> GetTrainingSessionsByPlan(Guid idTP)
        {
            return _configRutinaDB.SesionEntrenamientos
                .AsNoTracking()
                .Include(ts => ts.PlanEntrenamientoEn)
                .Where(ts => ts.IdPlanEntrenamiento == idTP)
                .ToList();
        }
    }
}
