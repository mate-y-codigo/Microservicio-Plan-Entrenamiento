using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingSession;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
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
        private readonly ITrainingSessionQuery _query;
        private readonly IExerciseSessionQuery _exerciseSessionQuery;
        private readonly ExerciseSessionMapper _exerciseSessionMapper;

        public TrainingSessionService(ITrainingSessionCommand command, IExerciseSessionService exerciseSessionService, TrainingSessionMapper mapper, TrainingSessionValidator validator, ITrainingSessionQuery query, IExerciseSessionQuery exerciseSessionQuery, ExerciseSessionMapper exerciseSessionMapper)
        {
            _command = command;
            _exerciseSessionService = exerciseSessionService;
            _mapper = mapper;
            _validator = validator;
            _query = query;
            _exerciseSessionQuery = exerciseSessionQuery;
            _exerciseSessionMapper = exerciseSessionMapper;
        }

        public async Task<SesionEntrenamiento> CreateAsync(Guid planId, TrainingSessionCreateRequest request)
        {
            await _validator.ValidateCreate(request);

            var trainingSession = _mapper.ToTrainingSession(planId, request);
            await _command.InsertTrainingSession(trainingSession);

            trainingSession.EjercicioSesionLista = new List<EjercicioSesion>();
            if (request.sesionesEjercicio != null)
            {
                foreach (var se in request.sesionesEjercicio.OrderBy(e => e.orden))
                {
                    var exerciseSession = await _exerciseSessionService.CreateAsync(trainingSession.Id, se);
                    trainingSession.EjercicioSesionLista.Add(exerciseSession);
                }
            }
            return trainingSession;
        }

        public async Task<List<TrainingSessionResponse>> GetAllTrainingSession()
        {
            var query = await _query.GetAllTrainingSessions();

            if (query == null)
                throw new NotFoundException("No se encontraron sesiones"); 
            
            var lista = new List<TrainingSessionResponse>();
            
            foreach (var session in query) {
                lista.Add(_mapper.ToResponseSinSesiones(session));
            }

            return lista;
        }

        public async Task<TrainingSessionWithPlanResponse> GetTrainingSessionById(Guid id)
        {
            var sesion = await _query.GetById(id);
            if (sesion is null)
                throw new NotFoundException("La sesión de entrenamiento no existe.");

            var ejercicios = await _exerciseSessionQuery.GetExerciseSessionsByTrainingSession(sesion.Id);
            var shorts = ejercicios
                .OrderBy(e => e.Orden)
                .Select(_exerciseSessionMapper.ToShortResponse)
                .ToList();

            return _mapper.ToWithPlanResponse(sesion, shorts);
        }
    }
}
