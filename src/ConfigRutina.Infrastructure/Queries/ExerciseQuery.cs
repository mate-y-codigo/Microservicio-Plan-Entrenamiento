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
                .Include(ce => ce.CategoriaEjercicioEn)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Ejercicio>> GetByFilter(string name, string mainMuscle, string muscleGroup, int category, bool active)
        {
            var query = _configRutinaDB.Ejercicios
                .AsNoTracking()
                .Include(ce => ce.CategoriaEjercicioEn)
                .AsQueryable();

            // filters
            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Nombre.ToLower().Contains(name.Trim().ToLower()));

            if (!string.IsNullOrEmpty(mainMuscle))
                query = query.Where(e => e.MusculoPrincipal.ToLower().Contains(mainMuscle.Trim().ToLower()));

            if (!string.IsNullOrEmpty(muscleGroup))
                query = query.Where(e => e.GrupoMuscular.ToLower().Contains(muscleGroup.Trim().ToLower()));

            if (category > 0)
                query = query.Where(e => e.IdCategoriaEjercicio == category);

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
