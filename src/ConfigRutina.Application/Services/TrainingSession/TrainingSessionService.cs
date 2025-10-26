using ConfigRutina.Application.DTOs.Request.TrainingSession;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.TrainingSession
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly ITrainingSessionCommand _command;
        private readonly IExerciseSessionService _exerciseSessionService;
        private readonly TrainingSessionMapper _mapper;
        private readonly TrainingSessionValidator _validator;

        public TrainingSessionService(ITrainingSessionCommand command, IExerciseSessionService exerciseSessionService, TrainingSessionMapper mapper, TrainingSessionValidator validator)
        {
            _command = command;
            _exerciseSessionService = exerciseSessionService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<SesionEntrenamiento> CreateAsync(Guid planId, TrainingSessionCreateRequest request)
        {
            _validator.ValidateCreate(request);

            var trainingSession = _mapper.ToTrainingSession(planId, request);
            await _command.InsertTrainingSession(trainingSession);

            trainingSession.EjercicioSesionLista = new List<EjercicioSesion>();
            if (request.sessionExerciseCreateRequests != null)
            {
                foreach (var se in request.sessionExerciseCreateRequests.OrderBy(e => e.orden))
                {
                    var exerciseSession = await _exerciseSessionService.CreateAsync(trainingSession.Id, se);
                    trainingSession.EjercicioSesionLista.Add(exerciseSession);
                }
            }
            return trainingSession;
        }

        public Task<TrainingSessionResponse> GetTrainingSessionById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
