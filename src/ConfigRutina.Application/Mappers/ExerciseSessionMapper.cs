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
        public static ExerciseSessionResponse ExerciseSessionToResponse(EjercicioSesion sesion) {
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
    }
}
