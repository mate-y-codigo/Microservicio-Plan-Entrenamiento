using ConfigRutina.Application.DTOs.Response.CategoryExercise;
using ConfigRutina.Application.DTOs.Response.MuscleGroup;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public static class MuscleGroupMapper
    {
        public static MuscleGroupResponse ToMuscleGroupResponse(GrupoMuscular gm)
        {
            return new MuscleGroupResponse()
            {
                id = gm.Id,
                nombre = gm.Nombre,
            };
        }
    }
}
