using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.ExerciseSession
{
    public class ExerciseSessionService : IExerciseSessionService
    {
        private readonly IExerciseSessionCommand _command;
        private readonly IExerciseSessionQuery _query;
        private readonly ExerciseSessionMapper _mapper;
        private readonly ExerciseSessionValidator _validator;

        public ExerciseSessionService(IExerciseSessionCommand command, IExerciseSessionQuery query, ExerciseSessionMapper mapper, ExerciseSessionValidator validator)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EjercicioSesion> CreateAsync(Guid trainingSessionId, SessionExerciseCreateRequest request)
        {
            _validator.ValidateCreate(request);

            var exerciseSession = _mapper.ToExerciseSession(trainingSessionId, request);
            await _command.InsertExerciseSession(exerciseSession);

            return exerciseSession;
        }

        public async Task<ExerciseSessionResponse> GetExerciseSessionById(Guid id)
        {
            var exerciseSession =  await _query.GetById(id);
            if (exerciseSession == null)
            {
                throw new NotFoundException($"No se encontró la sesión de ejercicio con Id: {id}");
            }
            return _mapper.ToResponse(exerciseSession);
        }
    }
}
