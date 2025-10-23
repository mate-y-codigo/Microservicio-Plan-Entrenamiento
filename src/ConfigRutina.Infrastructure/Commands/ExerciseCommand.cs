using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Commands
{
    public class ExerciseCommand : IExcerciseCommand
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public ExerciseCommand(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task Delete(Ejercicio exercise)
        {
            if (await _configRutinaDB.EjercicioSesiones.AnyAsync())
            {
                await _configRutinaDB.Ejercicios
                    .Where(d => d.Id == exercise.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.Activo, e => false));
            }
            else
            {
                _configRutinaDB.Ejercicios.Remove(exercise);
                await _configRutinaDB.SaveChangesAsync();
            }
        }

        public async Task Insert(Ejercicio exercise)
        {
            _configRutinaDB.Add(exercise);
            await _configRutinaDB.SaveChangesAsync();
        }

        public async Task Update(Ejercicio exercise)
        {
            await _configRutinaDB.Ejercicios
           .Where(d => d.Id == exercise.Id)
           .ExecuteUpdateAsync(setters => setters
               .SetProperty(e => e.Nombre, e => exercise.Nombre)
               .SetProperty(e => e.MusculoPrincipal, e => exercise.MusculoPrincipal)
               .SetProperty(e => e.GrupoMuscular, e => exercise.GrupoMuscular)
               .SetProperty(e => e.IdCategoriaEjercicio, e => exercise.IdCategoriaEjercicio)
               .SetProperty(e => e.UrlDemo, e => exercise.UrlDemo)
               .SetProperty(e => e.Activo, e => exercise.Activo));
        }
    }
}
