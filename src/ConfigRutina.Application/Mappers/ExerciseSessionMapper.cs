using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public class ExerciseSessionMapper
    {
        public ExerciseSessionResponse ToResponse(EjercicioSesion sesion) {
            var result = new ExerciseSessionResponse
            {
                id = sesion.Id,
                targetSets = sesion.SeriesObjetivo,
                targetReps = sesion.RepeticionesObjetivo,
                targetWeight = sesion.PesoObjetivo,
                rest = sesion.Descanso,
                order = sesion.Orden,
                exercise = new ExerciseResponse
                {
                    id = sesion.EjercicioEn.Id,
                    nombre = sesion.EjercicioEn.Nombre,
                    musculoPrincipal = sesion.EjercicioEn.MusculoPrincipal,
                    grupoMuscular = sesion.EjercicioEn.GrupoMuscular,
                    urlDemo = sesion.EjercicioEn.UrlDemo,
                    activo = sesion.EjercicioEn.Activo,
                    categoria = new CategoryExerciseResponse
                    {
                        id = sesion.EjercicioEn.CategoriaEjercicioEn.Id,
                        nombre = sesion.EjercicioEn.CategoriaEjercicioEn.Nombre,

                    }
                }
            };
            return result;
        }

        public EjercicioSesion ToExerciseSession(Guid trainingSessionId, SessionExerciseCreateRequest request)
        {
            var exerciseSession = new EjercicioSesion
            {
                Id = Guid.NewGuid(),
                IdSesionEntrenamiento = trainingSessionId,
                IdEjercicio = request.exerciseId,
                SeriesObjetivo = request.targetSets,
                RepeticionesObjetivo = request.targetReps,
                PesoObjetivo = request.targetWeight,
                Descanso = request.rest,
                Orden = request.order
            };
            return exerciseSession;
        }

        public ExerciseSessionShortResponse ToShortResponse(EjercicioSesion sesion)
        {
            return new ExerciseSessionShortResponse
            {
                id = sesion.Id,
                exerciseId = sesion.IdEjercicio
            };
        }
    }
}
