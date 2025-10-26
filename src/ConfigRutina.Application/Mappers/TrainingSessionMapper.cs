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
                Nombre = request.nombre,
                Orden = request.orden
            };
        }

        public TrainingSessionResponse ToResponse(SesionEntrenamiento session, List<ExerciseSessionShortResponse> exerciseSessions) {
            var response = new TrainingSessionResponse
            {
                id = session.Id,
                planEntrenamientoId = session.IdPlanEntrenamiento,
                nombre = session.Nombre,
                orden = session.Orden,
                sesionesEjercicio= exerciseSessions
            };
            return response;
        }
    }
}
