using ConfigRutina.Application.Interfaces.ExcerciseSession;
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
    public class ExerciseSessionQuery : IExerciseSessionQuery
    {
        private readonly ConfigRutinaDB _configRutinaDB;

        public ExerciseSessionQuery(ConfigRutinaDB configRutinaDB)
        {
            _configRutinaDB = configRutinaDB;
        }

        public async Task<EjercicioSesion> GetById(Guid id)
        {
            return _configRutinaDB.EjercicioSesiones.AsNoTracking()
                .Where(es => es.Id == id)
                .Include(es => es.EjercicioEn)
                .Include(es => es.SesionEntrenamientoEn)
                .FirstOrDefault();
        }

        // trae todos ejercicio de entrenamiento por sesion de entrenamiento
        public async Task<List<EjercicioSesion>> GetExerciseSessionsByTrainingSession(Guid idTS)
        {
            return _configRutinaDB.EjercicioSesiones.AsNoTracking()
                .Include(es => es.EjercicioEn)
                .Include(es => es.SesionEntrenamientoEn)
                .Where(es => es.IdSesionEntrenamiento == idTS).ToList();
        }
    }
}
