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
                IdEntrenador = request.idEntrenador,
                Nombre = request.nombre,
                Descripcion = request.descripcion,
                EsPlantilla = request.esPlantilla,
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
                nombre = plan.Nombre,
                descripcion = plan.Descripcion,
                esPlantilla = plan.EsPlantilla,
                idEntrenador = plan.IdEntrenador,
                fechaCreacion = plan.FechaCreacion,
                fechaActualizacion = plan.FechaActualizacion,
                activo = plan.Activo,
                sesionesEntrenamiento = sessions.OrderBy(s => s.orden).ToList()
            };
        }

        public TrainingPlanStatusResponse ToStatusResponse(PlanEntrenamiento plan)
        {
            return new TrainingPlanStatusResponse
            {
                id = plan.Id,
                nombre = plan.Nombre,
                fechaActualizacion = plan.FechaActualizacion,
                activo = plan.Activo
            };
        }
    }
}
