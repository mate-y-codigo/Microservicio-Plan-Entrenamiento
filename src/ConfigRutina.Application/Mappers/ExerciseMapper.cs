using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Application.DTOs.Response.Exercise;
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
                MusculoPrincipal = resquest.musculoPrincipal.Trim(),
                GrupoMuscular = resquest.grupoMuscular.Trim(),
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
                MusculoPrincipal = resquest.musculoPrincipal.Trim(),
                GrupoMuscular = resquest.grupoMuscular.Trim(),
                UrlDemo = resquest.urlDemo.Trim(),
                IdCategoriaEjercicio = resquest.categoriaEjercicio,
                Activo = resquest.activo,
            };
        }

        public static ExerciseResponse ToExerciseResponse(Ejercicio ejercicio)
        {
            return new ExerciseResponse()
            {
                id = ejercicio.Id,
                nombre = ejercicio.Nombre,
                musculoPrincipal = ejercicio.MusculoPrincipal,
                grupoMuscular = ejercicio.GrupoMuscular,
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
