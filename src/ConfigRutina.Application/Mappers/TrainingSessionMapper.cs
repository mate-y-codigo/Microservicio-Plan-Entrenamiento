using ConfigRutina.Application.DTOs.Request.TrainingSession;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Services.TrainingSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public class TrainingSessionMapper
    {
        public SesionEntrenamiento ToTrainingSession(Guid planId, TrainingSessionCreateRequest request)
        {
            return new SesionEntrenamiento
            {
                Id = Guid.NewGuid(),
                IdPlanEntrenamiento = planId,
                Nombre = request.name,
                Orden = request.order
            };
        }

        public TrainingSessionResponse ToResponse(SesionEntrenamiento session, List<ExerciseSessionShortResponse> exerciseSessions) {
            var response = new TrainingSessionResponse
            {
                id = session.Id,
                idTrainingPlan = session.IdPlanEntrenamiento,
                name = session.Nombre,
                order = session.Orden,
                exerciseSessions = exerciseSessions
            };
            return response;
        }
    }
}
