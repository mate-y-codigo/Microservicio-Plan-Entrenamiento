using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public static class CategoryExerciseMapper
    {
        public static CategoryExerciseResponse ToCategoryExerciseResponse(CategoriaEjercicio ce)
        {
            return new CategoryExerciseResponse()
            {
                id = ce.Id,
                nombre = ce.Nombre,
            };
        }
    }
}
