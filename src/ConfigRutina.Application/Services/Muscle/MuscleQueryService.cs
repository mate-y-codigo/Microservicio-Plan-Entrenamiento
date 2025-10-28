using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.Interfaces.Muscle;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConfigRutina.Application.Services.Muscle
{
    public class MuscleQueryService : IMuscleQueryService
    {
        private readonly IMuscleQuery<Musculo> _muscleQuery;
        private readonly IValidatorMuscleSearchRequest _validatorMuscleSearchRequest;

        public MuscleQueryService(IMuscleQuery<Musculo> muscleQuery, IValidatorMuscleSearchRequest validatorMuscleSearchRequest)
        {
            _muscleQuery = muscleQuery;
            _validatorMuscleSearchRequest = validatorMuscleSearchRequest;
        }

        public async Task<List<MuscleResponse>?> GetAll()
        {
            List<MuscleResponse> muscleResponse = new List<MuscleResponse>();

            var result = await _muscleQuery.GetAll();

            if (result != null)
            {
                foreach (var ce in result)
                    muscleResponse.Add(MuscleMapper.ToMuscleResponse(ce));

                return muscleResponse;
            }

            return null;
        }

        public async Task<List<MuscleResponse>?> Search(int idMuscle, int idMuscleGroup, string? muscle, string? muscleGroup)
        {
            List<MuscleResponse> muscleResponse = new List<MuscleResponse>();

            _validatorMuscleSearchRequest.Validate(idMuscle, idMuscleGroup, muscle, muscleGroup);
            var result = await _muscleQuery.GetByFilter(idMuscle, idMuscleGroup, muscle, muscleGroup);

            if (result != null)
            {
                foreach (var ce in result)
                    muscleResponse.Add(MuscleMapper.ToMuscleResponse(ce));

                return muscleResponse;
            }

            return null;
        }
    }
}
