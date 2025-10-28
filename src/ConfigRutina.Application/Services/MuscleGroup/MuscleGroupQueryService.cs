using ConfigRutina.Application.DTOs.Response.MuscleGroup;
using ConfigRutina.Application.Interfaces.MuscleGroup;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.MuscleGroup
{
    public class MuscleGroupQueryService : IMuscleGroupQueryService
    {
        private readonly IMuscleGroupQuery<List<GrupoMuscular>> _muscleGroup;

        public MuscleGroupQueryService(IMuscleGroupQuery<List<GrupoMuscular>> muscleGroup)
        {
            _muscleGroup = muscleGroup;
        }

        public async Task<List<MuscleGroupResponse>?> GetAll()
        {
            List<MuscleGroupResponse> muscleGroupResponse = new List<MuscleGroupResponse>();

            var result = await _muscleGroup.GetAll();

            if (result != null)
            {
                foreach (var mg in result)
                    muscleGroupResponse.Add(MuscleGroupMapper.ToMuscleGroupResponse(mg));

                return muscleGroupResponse;
            }

            return null;
        }
    }
}
