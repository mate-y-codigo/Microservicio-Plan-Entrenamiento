using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
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
    public class ExerciseSessionCommand : IExerciseSessionCommand
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public ExerciseSessionCommand(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task InsertExerciseSession(EjercicioSesion es)
        {
            //meto validación de ejercicio acá para no romper el código
            var exerciseExists = await _configRutinaDB.Ejercicios
            .AsNoTracking()
            .AnyAsync(e => e.Id == es.IdEjercicio);
            if (!exerciseExists)
            {
                throw new NotFoundException($"El ejercicio con ID '{es.IdEjercicio}' no existe.");
            }
            var sessionExists = await _configRutinaDB.SesionEntrenamientos
                .AsNoTracking()
                .AnyAsync(s => s.Id == es.IdSesionEntrenamiento);
            if (!sessionExists)
            {
                throw new NotFoundException($"La sesión de entrenamiento con ID '{es.IdSesionEntrenamiento}' no existe.");
            }

            _configRutinaDB.Add(es);
            await _configRutinaDB.SaveChangesAsync();
        }

        public async Task UpdateExerciseSession(EjercicioSesion es)
        {
            await _configRutinaDB.EjercicioSesiones.Where(n => n.Id == es.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(n => n.SeriesObjetivo, es.SeriesObjetivo)
                .SetProperty(n => n.RepeticionesObjetivo, es.RepeticionesObjetivo)
                .SetProperty(n => n.PesoObjetivo, es.PesoObjetivo)
                .SetProperty(n => n.Descanso,es.Descanso)
                .SetProperty(n => n.Orden, es.Orden)
                .SetProperty(n => n.IdSesionEntrenamiento, es.IdSesionEntrenamiento)
                .SetProperty(n => n.IdEjercicio, es.IdEjercicio)
                );
        }
    }
}
