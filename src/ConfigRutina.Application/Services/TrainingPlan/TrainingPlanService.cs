using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Enums;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IValidatorTrainingPlanPatchStatusRequest _validatortrainingPlanPatchStatusRequest;
        private readonly IValidateTrainingPlanDelete _trainingPlanDeleteValidator;
        private readonly IValidateTrainingPlanUpdate _trainingPlanUpdateValidator;
        private readonly IExerciseSessionCommand _exerciseSessionCommand;
        public TrainingPlanService(
            ITrainingPlanCommand command,
            ITrainingPlanQuery query,
            TrainingPlanMapper mapper,
            TrainingPlanValidator validator,
            ITrainingSessionService trainingSessionService,
            TrainingSessionMapper trainingSessionMapper,
            ExerciseSessionMapper exerciseSessionMapper,
            TrainingSessionValidator trainingSessionValidator,
            ExerciseSessionValidator exerciseSessionValidator,
            ITrainingPlanAggregateCommand aggregateCommand,
            ITrainingSessionQuery trainingSessionQuery,
            IExerciseSessionQuery exerciseSessionQuery,
            IValidatorTrainingPlanPatchStatusRequest validatortrainingPlanPatchStatusRequest,
            IValidateTrainingPlanDelete ValidatorTrainingPlanDeleteValidator,
            IValidateTrainingPlanUpdate ValidatorTrainingPlanUpdate,
            IExerciseSessionCommand exerciseSessionCommand)

            
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
            _validatortrainingPlanPatchStatusRequest = validatortrainingPlanPatchStatusRequest;
            _trainingPlanDeleteValidator = ValidatorTrainingPlanDeleteValidator;
            _trainingPlanUpdateValidator = ValidatorTrainingPlanUpdate;
            _exerciseSessionCommand = exerciseSessionCommand;
        }

        public async Task<TrainingPlanResponse> CreateTrainingPlan(CreateTrainingPlanRequest request)
        {
            // Validaciones de forma (sin BD)
            await _validator.ValidateCreate(request);
            foreach (var s in request.sesionesEntrenamiento)
            {
                await _trainingSessionValidator.ValidateCreate(s);
            }
            foreach (var e in request.sesionesEntrenamiento.SelectMany(s => s.sesionesEjercicio))
            {
                _exerciseSessionValidator.ValidateCreate(e);
            }

            // Mapear con IDs ya generados todavía sin guardar
            var plan = _mapper.ToTrainingPlan(request);

            var sessionEntities = new List<SesionEntrenamiento>();
            var exerciseEntities = new List<EjercicioSesion>();

            foreach (var s in request.sesionesEntrenamiento.OrderBy(x => x.orden))
            {
                var session = _trainingSessionMapper.ToTrainingSession(plan.Id, s);
                sessionEntities.Add(session);

                if (s.sesionesEjercicio != null)
                {
                    foreach (var ex in s.sesionesEjercicio.OrderBy(e => e.orden))
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

        public async Task<TrainingPlanStatusResponse>UpdateTrainingPlan(string id,UpdateTrainingPlanRequest request,bool IsAsigned)
        {
            await _trainingPlanUpdateValidator.validate(id, request, IsAsigned);
            Guid IdPlan;
            Guid.TryParse(id, out IdPlan);
           
            foreach (var trainingSession in request.sesionEntrenamientos){
                List<EjercicioSesion> listaReales = new List<EjercicioSesion>();
                listaReales.AddRange(await _exerciseSessionQuery.GetExerciseSessionsByTrainingSession(trainingSession.IdSesionEntrenamiento));
                foreach (var exerciseSession in trainingSession.EjercicioSesiones) {
                    if (!await _exerciseSessionQuery.metodo(trainingSession.IdSesionEntrenamiento, exerciseSession.idEjercicio))
                    {
                        var ejercicio = new EjercicioSesion
                        {   
                            
                            IdSesionEntrenamiento = trainingSession.IdSesionEntrenamiento,
                            IdEjercicio = exerciseSession.idEjercicio,
                            Orden = exerciseSession.orden,
                            Descanso = exerciseSession.descanso,
                            PesoObjetivo = exerciseSession.pesoObjetivo,
                            RepeticionesObjetivo = exerciseSession.repeticionesObjetivo,
                            SeriesObjetivo = exerciseSession.seriesObjetivo
                        };
                       
                        await _exerciseSessionCommand.InsertExerciseSession(ejercicio);
                    }
                    else {
                        var ejercicio = new EjercicioSesion
                        {
                            Id = exerciseSession.id,
                            IdSesionEntrenamiento = trainingSession.IdSesionEntrenamiento,
                            IdEjercicio = exerciseSession.idEjercicio,
                            Orden = exerciseSession.orden,
                            Descanso = exerciseSession.descanso,
                            PesoObjetivo = exerciseSession.pesoObjetivo,
                            RepeticionesObjetivo = exerciseSession.repeticionesObjetivo,
                            SeriesObjetivo = exerciseSession.seriesObjetivo
                        };
                        await _exerciseSessionCommand.UpdateExerciseSession(ejercicio);

                    }
                    
                }

            }
            var query =  await _query.GetTrainingPlanById(IdPlan);

            return _mapper.ToStatusResponse(query);

        }

        public async Task<TrainingPlanStatusResponse> SetStatus(string? strId, bool status)
        {
            Guid id;
            Guid.TryParse(strId, out id);

            await _validatortrainingPlanPatchStatusRequest.Validate(strId);

            await _command.UpdateStatusTrainingPlan(id, status);
            return _mapper.ToStatusResponse((await _query.GetTrainingPlanById(id))!);
        }

        public async Task<TrainingPlanResponse> DeleteTrainingPlan(string id,bool IsUsed)
        {
            Guid ID;
            await _trainingPlanDeleteValidator.Validate(id, IsUsed);
            Guid.TryParse(id, out ID);
            var query = await _query.GetTrainingPlanById(ID);
  
            var sessions = await _trainingSessionQuery.GetTrainingSessionsByPlan(query.Id);
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
            await _command.DeleteTrainingPlan(ID);
            return _mapper.ToResponse(query,sessionResponses);
        }
    }
}
