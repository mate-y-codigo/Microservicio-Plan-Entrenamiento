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

        public async Task InsertExcerciseSession(EjercicioSesion es)
        {
            _configRutinaDB.Add(es);
            await _configRutinaDB.SaveChangesAsync();
        }

        public async Task UpdateExcerciseSession(EjercicioSesion es)
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
