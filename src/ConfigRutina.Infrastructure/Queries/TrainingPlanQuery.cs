using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigRutina.Infrastructure.Queries
{
    public class TrainingPlanQuery : ITrainingPlanQuery
    {
        private readonly ConfigRutinaDB _configRutinaDB;
        public TrainingPlanQuery(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task<PlanEntrenamiento> GetTrainingPlanById(Guid id)
        {
            return _configRutinaDB.PlanEntrenamientos.AsNoTracking()
                .Where(pe => pe.Id == id)
                .FirstOrDefault();
        }

        public async Task<List<PlanEntrenamiento>> GetTrainingPLanFilter(DateTime? createDate, DateTime? updateDate, Guid? idEntrenador, string? name, bool plantilla = true, bool active = true)
        {
            var query = _configRutinaDB.PlanEntrenamientos
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(pe => pe.Nombre.ToLower().Contains(name.ToLower()));

            if (plantilla)
                query = query.Where(pe => pe.EsPlantilla);

            if (idEntrenador != null && idEntrenador != Guid.Empty)
                query = query.Where(pe => pe.IdEntrenador == idEntrenador);

            if (active)
                query = query.Where(pe => pe.Activo);


            if (createDate.HasValue && !updateDate.HasValue)
                query = query.Where(pe => pe.FechaCreacion >= updateDate.Value);

            if (!createDate.HasValue && updateDate.HasValue)
                query = query.Where(pe => pe.FechaCreacion <= updateDate.Value);

            if (createDate.HasValue && updateDate.HasValue)
                query = query.Where(pe => pe.FechaCreacion >= updateDate.Value && pe.FechaCreacion <= updateDate.Value);

          
            return await query.ToListAsync();
        }
    }
}
