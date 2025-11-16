using ConfigRutina.Application.Interfaces.Muscle;
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
    public class MuscleQuery : IMuscleQuery<Musculo>
    {
        public readonly ConfigRutinaDB _configRutina;

        public MuscleQuery(ConfigRutinaDB configRutina)
        {
            _configRutina = configRutina ?? throw new ArgumentNullException(nameof(configRutina));
        }

        public async Task<int> GetCount()
        {
           return await _configRutina.Musculos.CountAsync();
        }

        public async Task<List<Musculo>> GetAll()
        {
            return await _configRutina.Musculos.
                AsNoTracking().
                Include(m => m.GrupoMuscularEn).
                OrderBy(m => m.Id).
                ToListAsync();
        }

        public async Task<Musculo?> SearchById(int id)
        {
            return await _configRutina.Musculos.
               AsNoTracking().
               Include(m => m.GrupoMuscularEn).
               FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Musculo>> GetByFilter(int? idMuscle, int? idMuscleGroup, string? muscle, string? muscleGroup)
        {
            var query = _configRutina.Musculos.
               AsNoTracking().
               Include(m => m.GrupoMuscularEn).
               OrderBy(m => m.Id).
               AsQueryable();

            // filters
            if(idMuscle != 0)
                query = query.Where(e => e.Id == idMuscle);

            if (idMuscleGroup != 0)
                query = query.Where(e => e.GrupoMuscularEn!.Id == idMuscleGroup);

            if (!string.IsNullOrEmpty(muscle))
                query = query.Where(e => e.Nombre.ToLower().Contains(muscle.Trim().ToLower()));

            if (!string.IsNullOrEmpty(muscleGroup))
                query = query.Where(e => e.GrupoMuscularEn!.Nombre.ToLower().Contains(muscleGroup.Trim().ToLower()));

            return await query.ToListAsync();
        }
    }
}
