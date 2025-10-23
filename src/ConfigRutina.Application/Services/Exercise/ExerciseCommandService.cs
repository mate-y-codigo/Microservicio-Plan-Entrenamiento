using ConfigRutina.Application.DTOs.Request.Exercise;
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

namespace ConfigRutina.Application.Services.Exercise
{
    public class ExerciseCommandService : IExcerciseCommandService
    {
        private readonly IValidatorExerciseCreateRequest _validatorExerciseCreateRequest;
        private readonly IValidateExerciseUpdateRequest _validateExerciseUpdateRequest;
        private readonly IValidatorExerciseDeleteRequest _validatorExerciseDeleteRequest;
        private readonly IExcerciseCommand _excerciseCommand;
        private readonly IExcerciseQuery<Ejercicio> _excerciseQuery;

        public ExerciseCommandService(
            IValidatorExerciseCreateRequest validatorExerciseCreateRequest,
            IValidateExerciseUpdateRequest validateExerciseUpdateRequest,
            IValidatorExerciseDeleteRequest validatorExerciseDeleteRequest,
            IExcerciseCommand excerciseCommand,
            IExcerciseQuery<Ejercicio> excerciseQuery)
        {
            _validatorExerciseCreateRequest = validatorExerciseCreateRequest;
            _validateExerciseUpdateRequest = validateExerciseUpdateRequest;
            _validatorExerciseDeleteRequest = validatorExerciseDeleteRequest;
            _excerciseCommand = excerciseCommand;
            _excerciseQuery = excerciseQuery;
        }

        public async Task<ExerciseResponse> Create(ExerciseCreateRequest request)
        {
            await _validatorExerciseCreateRequest.Validate(request);
            var exercise = ExerciseMapper.ToExercise(request);
            await _excerciseCommand.Insert(exercise);
            return ExerciseMapper.ToExerciseResponse((await _excerciseQuery.GetById(exercise.Id))!);
        }

        public async Task<ExerciseResponse> Delete(string strId)
        {
            Guid id = await _validatorExerciseDeleteRequest.Validate(strId);
            Ejercicio exercise = (await _excerciseQuery.GetById(id))!;
            await _excerciseCommand.Delete(exercise);
            return ExerciseMapper.ToExerciseResponse(exercise);
        }

        public async Task<ExerciseResponse> Update(string id, ExerciseUpdateRequest request)
        {
            await _validateExerciseUpdateRequest.Validate(id, request);
            var exercise = ExerciseMapper.ToExercise(id, request);
            await _excerciseCommand.Update(exercise);
            return ExerciseMapper.ToExerciseResponse((await _excerciseQuery.GetById(Guid.Parse(id)))!);
        }
    }
}
