using ConfigRutina.Application.DTOs.Response.CategoryExercise;
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
    public static class MuscleMapper
    {
        public static MuscleResponse ToMuscleResponse(Musculo m)
        {
            return new MuscleResponse()
            {
                id = m.Id,
                nombre = m.Nombre,
                grupoMuscular = new MuscleGroupResponse()
                {
                    id = m.GrupoMuscularEn!.Id,
                    nombre = m.GrupoMuscularEn!.Nombre
                }
            };
        }
    }
}
