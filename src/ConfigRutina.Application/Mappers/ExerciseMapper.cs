using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Response.CategoryExercise;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.DTOs.Response.MuscleGroup;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConfigRutina.Application.Mappers
{
    public static class ExerciseMapper
    {
        public static Ejercicio ToExercise(ExerciseCreateRequest resquest)
        {
            return new Ejercicio()
            {
                Nombre = resquest.nombre.Trim(),
                IdMusculo = resquest.musculo,
                UrlDemo = resquest.urlDemo.Trim(),
                IdCategoriaEjercicio = resquest.categoriaEjercicio,
                Activo = true,
            };
        }

        public static Ejercicio ToExercise(string id, ExerciseUpdateRequest resquest)
        {
            return new Ejercicio()
            {
                Id = Guid.Parse(id),
                Nombre = resquest.nombre.Trim(),
                IdMusculo = resquest.musculo,
                UrlDemo = resquest.urlDemo.Trim(),
                IdCategoriaEjercicio = resquest.categoriaEjercicio,
                Activo = resquest.activo,
            };
        }

        public static ExerciseResponse ToExerciseResponse(Ejercicio ejercicio)
        {
            MuscleGroupResponse grupoMuscularResponse = new MuscleGroupResponse();

            if (ejercicio.MusculoEn?.GrupoMuscularEn != null)
            {
                grupoMuscularResponse = new MuscleGroupResponse
                {
                    id = ejercicio.MusculoEn.GrupoMuscularEn.Id,
                    nombre = ejercicio.MusculoEn.GrupoMuscularEn.Nombre
                };
            }

            return new ExerciseResponse()
            {
                id = ejercicio.Id,
                nombre = ejercicio.Nombre,
                musculo = ejercicio.MusculoEn != null ?
                new MuscleResponse
                {
                    id = ejercicio.MusculoEn.Id,
                    nombre = ejercicio.MusculoEn.Nombre,
                    grupoMuscular = grupoMuscularResponse,
                }
                : null,
                urlDemo = ejercicio.UrlDemo != null ? ejercicio.UrlDemo : "",
                activo = ejercicio.Activo,
                categoria = ejercicio.CategoriaEjercicioEn != null ?
                new CategoryExerciseResponse
                {
                    id = ejercicio.CategoriaEjercicioEn.Id,
                    nombre = ejercicio.CategoriaEjercicioEn.Nombre
                }
                : null,
            };
        }
    }
}
