using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConfigRutina.Application.Services.Exercise
{
    public class ExerciseQueryService : IExcerciseQueryService
    {
        private readonly IExcerciseQuery<Ejercicio> _excerciseQuery;
        private readonly IValidatorExerciseSearchRequest _validatorExerciseSearchRequest;
        private readonly IValidatorExerciseSearchByIdRequest _validatorExerciseSearchByIdRequest;

        public ExerciseQueryService(
            IExcerciseQuery<Ejercicio> excerciseQuery,
            IValidatorExerciseSearchRequest validatorExerciseSearchRequest,
            IValidatorExerciseSearchByIdRequest validatorExerciseSearchByIdRequest)
        {
            _excerciseQuery = excerciseQuery;
            _validatorExerciseSearchRequest = validatorExerciseSearchRequest;
            _validatorExerciseSearchByIdRequest = validatorExerciseSearchByIdRequest;
        }

        public async Task<List<ExerciseResponse>> Search(string? name, int muscle, int category, bool active)
        {
            await _validatorExerciseSearchRequest.Validate(name, muscle, category);
            List<Ejercicio>? result = await _excerciseQuery.GetByFilter(name, muscle, category, active);
            List<ExerciseResponse> exerciseResponses = new List<ExerciseResponse>();

            if (result != null && result.Count > 0)
            {
                foreach (var e in result)
                    exerciseResponses.Add(ExerciseMapper.ToExerciseResponse(e));

                return exerciseResponses;
            }
            return exerciseResponses;
        }

        public async Task<ExerciseResponse> SearchById(string strId)
        {
            Guid id = await _validatorExerciseSearchByIdRequest.Validate(strId);
            return ExerciseMapper.ToExerciseResponse((await _excerciseQuery.GetById(id))!);
        }
    }
}
