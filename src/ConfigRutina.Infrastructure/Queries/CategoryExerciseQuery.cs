using ConfigRutina.Application.Interfaces.CategoryExcercise;
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
    public class CategoryExerciseQuery : ICategoryExcerciseQuery<List<CategoriaEjercicio>>
    {
        private readonly ConfigRutinaDB _configRutina;

        public CategoryExerciseQuery(ConfigRutinaDB configRutinaDB)
        {
            _configRutina = configRutinaDB;
        }

        public async Task<List<CategoriaEjercicio>> GetAll()
        {
            return await _configRutina.CategoriaEjercicios.AsNoTracking().ToListAsync();
        }
    }
}
