using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.DTOs.Response.CategoryExercise;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.DTOs.Response.MuscleGroup;
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
        public ExerciseSessionResponse ToResponse(EjercicioSesion sesion)
        {
            MuscleGroupResponse grupoMuscularResponse = new MuscleGroupResponse();

            if (sesion.EjercicioEn!.MusculoEn?.GrupoMuscularEn != null)
            {
                grupoMuscularResponse = new MuscleGroupResponse
                {
                    id = sesion.EjercicioEn.MusculoEn.GrupoMuscularEn.Id,
                    nombre = sesion.EjercicioEn.MusculoEn.GrupoMuscularEn.Nombre
                };
            }

            var result = new ExerciseSessionResponse
            {
                id = sesion.Id,
                seriesObjetivo = sesion.SeriesObjetivo,
                repeticionesObjetivo = sesion.RepeticionesObjetivo,
                pesoObjetivo = sesion.PesoObjetivo,
                descanso = sesion.Descanso,
                orden = sesion.Orden,
                ejercicio = new ExerciseResponse
                {
                    id = sesion.EjercicioEn.Id,
                    nombre = sesion.EjercicioEn.Nombre,
                    musculo = sesion.EjercicioEn.MusculoEn != null ?
                    new MuscleResponse
                    {
                        id = sesion.EjercicioEn.MusculoEn.Id,
                        nombre = sesion.EjercicioEn.MusculoEn.Nombre,
                        grupoMuscular = grupoMuscularResponse,
                    }
                    : null,
                    urlDemo = sesion.EjercicioEn.UrlDemo != null ? sesion.EjercicioEn.UrlDemo : "",
                    activo = sesion.EjercicioEn.Activo,
                    categoria = sesion.EjercicioEn.CategoriaEjercicioEn != null ?
                    new CategoryExerciseResponse
                    {
                        id = sesion.EjercicioEn.CategoriaEjercicioEn.Id,
                        nombre = sesion.EjercicioEn.CategoriaEjercicioEn.Nombre
                    }
                    : null,
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
                IdEjercicio = request.id,
                SeriesObjetivo = request.seriesObjetivo,
                RepeticionesObjetivo = request.repeticionesObjetivo,
                PesoObjetivo = request.pesoObjetivo,
                Descanso = request.descanso,
                Orden = request.orden
            };
            return exerciseSession;
        }

        public ExerciseSessionShortResponse ToShortResponse(EjercicioSesion sesion)
        {
            return new ExerciseSessionShortResponse
            {
                id = sesion.Id,
                idEjercicio = sesion.IdEjercicio,
                nombreEjercicio = sesion.EjercicioEn?.Nombre ?? string.Empty,
                seriesObjetivo = sesion.SeriesObjetivo,
                repeticionesObjetivo = sesion.RepeticionesObjetivo,
                pesoObjetivo = sesion.PesoObjetivo,
                descanso = sesion.Descanso
            };
        }
    }
}
