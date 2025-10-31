using ConfigRutina.Application.Enums;
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

        public async Task<bool> ExistsTrainingPlan(Guid id)
        {
            var result = await _configRutinaDB.PlanEntrenamientos
                .Where(pe => pe.Id == id)
                .CountAsync();

            return result > 0;
        }

        public async Task<PlanEntrenamiento> GetTrainingPlanById(Guid id)
        {
            return await _configRutinaDB.PlanEntrenamientos
                .AsNoTracking()
                .FirstOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task<List<PlanEntrenamiento>> GetTrainingPlanFilter(string? name, bool? isTemplate, Guid? trainerId, bool? active, DateTime? from, DateTime? to, TrainingPlanOrderBy orderBy)
        {
            var query = _configRutinaDB.PlanEntrenamientos
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(pe => pe.Nombre.ToLower().Contains(name.ToLower()));
            }
            if (isTemplate.HasValue)
            {
                query = query.Where(pe => pe.EsPlantilla == isTemplate.Value);
            }
            if (trainerId.HasValue && trainerId != Guid.Empty)
            {
                query = query.Where(pe => pe.IdEntrenador == trainerId);
            }
            if (!active.HasValue || active.Value)
            {
                query = query.Where(pe => pe.Activo);
            }
            if (from.HasValue && !to.HasValue)
            {
                // desde from hasta la más nueva
                query = query.Where(pe => pe.FechaActualizacion >= from.Value);
            }
            else if (!from.HasValue && to.HasValue)
            {
                // desde el inicio hasta "to"
                query = query.Where(pe => pe.FechaActualizacion <= to.Value);
            }
            else if (from.HasValue && to.HasValue)
            {
                // Entre ambas
                var fromDate = from.Value;
                var toDate = to.Value;
                if (fromDate > toDate)
                {
                    (fromDate, toDate) = (toDate, fromDate); // invertir si están al revés
                }

                query = query.Where(pe => pe.FechaActualizacion >= fromDate && pe.FechaActualizacion <= toDate);
            }
            switch (orderBy)
            {
                case TrainingPlanOrderBy.name_asc:
                    query = query.OrderBy(pe => pe.Nombre);
                    break;
                case TrainingPlanOrderBy.name_desc:
                    query = query.OrderByDescending(pe => pe.Nombre);
                    break;
                case TrainingPlanOrderBy.createdate_asc:
                    query = query.OrderBy(pe => pe.FechaCreacion);
                    break;
                case TrainingPlanOrderBy.createdate_desc:
                    query = query.OrderByDescending(pe => pe.FechaCreacion);
                    break;
                case TrainingPlanOrderBy.updatedate_asc:
                    query = query.OrderBy(pe => pe.FechaActualizacion);
                    break;
                case TrainingPlanOrderBy.updatedate_desc:
                    query = query.OrderByDescending(pe => pe.FechaActualizacion);
                    break;
                default:
                    query = query.OrderByDescending(pe => pe.FechaCreacion);
                    break;
            }

            return await query.ToListAsync();
        }
    }
}
