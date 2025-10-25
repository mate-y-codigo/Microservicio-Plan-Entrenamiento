using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public class TrainingPlanMapper
    {
        public PlanEntrenamiento ToTrainingPlan(CreateTrainingPlanRequest request)
        {
            return new PlanEntrenamiento
            {
                Id = Guid.NewGuid(),
                IdEntrenador = request.trainerId,
                Nombre = request.name,
                Descripcion = request.description,
                EsPlantilla = request.isTemplate,
                Activo = true,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };
        }

        public TrainingPlanResponse ToResponse(PlanEntrenamiento plan, IEnumerable<TrainingSessionResponse> sessions)
        {
            return new TrainingPlanResponse
            {
                id = plan.Id,
                name = plan.Nombre,
                description = plan.Descripcion,
                isTemplate = plan.EsPlantilla,
                trainerId = plan.IdEntrenador,
                createDate = plan.FechaCreacion,
                updateDate = plan.FechaActualizacion,
                active = plan.Activo,
                trainingSessions = sessions.OrderBy(s => s.order).ToList()
            };
        }
    }
}
