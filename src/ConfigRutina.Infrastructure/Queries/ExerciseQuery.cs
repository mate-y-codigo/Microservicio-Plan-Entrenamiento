using ConfigRutina.Application.Interfaces.Excercise;
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
    public class ExerciseQuery : IExcerciseQuery<Ejercicio>
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public ExerciseQuery(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }
        
        public async Task<Ejercicio?> GetById(Guid id)
        {
            return await _configRutinaDB.Ejercicios
                .AsNoTracking()
                .Include(e => e.CategoriaEjercicioEn)
                .Include(e => e.MusculoEn)
                .Include(e => e.MusculoEn!.GrupoMuscularEn)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Ejercicio>?> GetByFilter(string? name, int idMuscle, int idCategory, bool active)
        {
            var query = _configRutinaDB.Ejercicios
                .AsNoTracking()
                .Include(e => e.CategoriaEjercicioEn)
                .Include(e => e.MusculoEn)
                .Include(e => e.MusculoEn!.GrupoMuscularEn)
                .AsQueryable();

            // filters
            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Nombre.ToLower().Contains(name.Trim().ToLower()));

            if (idMuscle > 0)
                query = query.Where(e => e.MusculoEn!.Id == idMuscle);

            if (idCategory > 0)
                query = query.Where(e => e.CategoriaEjercicioEn!.Id == idCategory);

            query = query.Where(e => e.Activo == active);

            return await query.ToListAsync();
        }

        public async Task<bool> ExistsById(Guid id)
        {
            var result = await _configRutinaDB.Ejercicios
                .Where(d => d.Id == id)
                .CountAsync();

            return result > 0;
        }

        public async Task<bool> ExistsByName(string name)
        {
            var result = await _configRutinaDB.Ejercicios
                .Where(d => d.Nombre.ToLower() == name.Trim().ToLower())
                .CountAsync();

            return result > 0;
        }
    }
}
