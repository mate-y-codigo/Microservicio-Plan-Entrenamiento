using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Validators;
using ConfigRutina.Application.Enums;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.TrainingPlan
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ITrainingPlanCommand _command;
        private readonly ITrainingPlanQuery _query;
        private readonly TrainingPlanMapper _mapper;
        private readonly TrainingPlanValidator _validator;
        private readonly ITrainingSessionService _trainingSessionService;
        private readonly TrainingSessionMapper _trainingSessionMapper;
        private readonly ExerciseSessionMapper _exerciseSessionMapper;
        private readonly TrainingSessionValidator _trainingSessionValidator;
        private readonly ExerciseSessionValidator _exerciseSessionValidator;
        private readonly ITrainingPlanAggregateCommand _aggregateCommand;
        private readonly ITrainingSessionQuery _trainingSessionQuery;
        private readonly IExerciseSessionQuery _exerciseSessionQuery;

        public TrainingPlanService(ITrainingPlanCommand command, ITrainingPlanQuery query, TrainingPlanMapper mapper, TrainingPlanValidator validator, ITrainingSessionService trainingSessionService, TrainingSessionMapper trainingSessionMapper, ExerciseSessionMapper exerciseSessionMapper, TrainingSessionValidator trainingSessionValidator, ExerciseSessionValidator exerciseSessionValidator, ITrainingPlanAggregateCommand aggregateCommand, ITrainingSessionQuery trainingSessionQuery, IExerciseSessionQuery exerciseSessionQuery)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _validator = validator;
            _trainingSessionService = trainingSessionService;
            _trainingSessionMapper = trainingSessionMapper;
            _exerciseSessionMapper = exerciseSessionMapper;
            _trainingSessionValidator = trainingSessionValidator;
            _exerciseSessionValidator = exerciseSessionValidator;
            _aggregateCommand = aggregateCommand;
            _trainingSessionQuery = trainingSessionQuery;
            _exerciseSessionQuery = exerciseSessionQuery;
        }

        public async Task<TrainingPlanResponse> CreateTrainingPlan(CreateTrainingPlanRequest request)
        {
            // Validaciones de forma (sin BD)
            await _validator.ValidateCreate(request);
            foreach (var s in request.TrainingSessionCreateRequests)
            {
                await _trainingSessionValidator.ValidateCreate(s);
            }
            foreach (var e in request.TrainingSessionCreateRequests.SelectMany(s => s.sessionExerciseCreateRequests))
            {
                _exerciseSessionValidator.ValidateCreate(e);
            }

            // Mapear con IDs ya generados todavía sin guardar
            var plan = _mapper.ToTrainingPlan(request);

            var sessionEntities = new List<SesionEntrenamiento>();
            var exerciseEntities = new List<EjercicioSesion>();

            foreach (var s in request.TrainingSessionCreateRequests.OrderBy(x => x.orden))
            {
                var session = _trainingSessionMapper.ToTrainingSession(plan.Id, s);
                sessionEntities.Add(session);

                if (s.sessionExerciseCreateRequests != null)
                {
                    foreach (var ex in s.sessionExerciseCreateRequests.OrderBy(e => e.orden))
                    {
                        var exEntity = _exerciseSessionMapper.ToExerciseSession(session.Id, ex);
                        exerciseEntities.Add(exEntity);
                    }
                }
            }

            // Validaciones de BD + Inserción
            await _aggregateCommand.CreatePlanCompleteAsync(plan, sessionEntities, exerciseEntities);

            // Armado del response
            var sessionResponses = sessionEntities
                .OrderBy(se => se.Orden)
                .Select(se =>
                {
                    var exShort = exerciseEntities
                        .Where(e => e.IdSesionEntrenamiento == se.Id)
                        .OrderBy(e => e.Orden)
                        .Select(_exerciseSessionMapper.ToShortResponse)
                        .ToList();

                    return _trainingSessionMapper.ToResponse(se, exShort);
                })
                .ToList();

            return _mapper.ToResponse(plan, sessionResponses);
        }

        public TrainingPlanStatusResponse ChangeStateTrainingPlan(string id,UpdateTrainingPlanStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TrainingPlanResponse>> GetFilterTrainingPlan(string? name, bool? isTemplate, Guid? trainerId, bool? active, DateTime? from, DateTime? to, TrainingPlanOrderBy orderBy)
        {
            var trainingPlans = await _query.GetTrainingPlanFilter(name, isTemplate, trainerId, active, from, to, orderBy);

            var responses = new List<TrainingPlanResponse>();

            foreach (var plan in trainingPlans)
            {
                var trainingSessions = await _trainingSessionQuery.GetTrainingSessionsByPlan(plan.Id);
                var sessionResponses = new List<TrainingSessionResponse>();

                foreach (var session in trainingSessions.OrderBy(ts => ts.Orden))
                {
                    var exerciseSessions = await _exerciseSessionQuery.GetExerciseSessionsByTrainingSession(session.Id);
                    var exercisesShort = exerciseSessions
                        .OrderBy(es => es.Orden)
                        .Select(_exerciseSessionMapper.ToShortResponse)
                        .ToList();

                    sessionResponses.Add(_trainingSessionMapper.ToResponse(session, exercisesShort));
                }
                responses.Add(_mapper.ToResponse(plan, sessionResponses));
            }
            return responses;
        }

        public async Task<TrainingPlanResponse> GetTrainingPlanById(Guid id)
        {
            var plan = await _query.GetTrainingPlanById(id);
            if (plan is null)
            {
                throw new NotFoundException("El plan de entrenamiento no existe.");
            }

            var sessions = await _trainingSessionQuery.GetTrainingSessionsByPlan(plan.Id);
            var Orderedsessions = (sessions ?? new List<SesionEntrenamiento>())
                .OrderBy(s => s.Orden)
                .ToList();

            var sessionResponses = new List<TrainingSessionResponse>();
            foreach (var session in Orderedsessions)
            {
                var exercises = await _exerciseSessionQuery.GetExerciseSessionsByTrainingSession(session.Id);
                var exercisesShort = (exercises ?? new List<EjercicioSesion>())
                    .OrderBy(e => e.Orden)
                    .Select(_exerciseSessionMapper.ToShortResponse)
                    .ToList();

                sessionResponses.Add(_trainingSessionMapper.ToResponse(session, exercisesShort));
            }

            return _mapper.ToResponse(plan, sessionResponses);
        }

        public TrainingPlanResponse UpdateTrainingPlan()
        {
            throw new NotImplementedException();
        }
    }
}
