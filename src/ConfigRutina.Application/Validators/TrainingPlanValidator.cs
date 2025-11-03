using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class TrainingPlanValidator
    {
        private readonly TrainingSessionValidator _trainingSessionValidator;
        public TrainingPlanValidator(TrainingSessionValidator trainingSessionValidator)
        {
            _trainingSessionValidator = trainingSessionValidator;
        }
        public async Task ValidateCreate(CreateTrainingPlanRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException("El request no puede ser nulo.");
            }
            if (request.idEntrenador == Guid.Empty)
            {
                throw new BadRequestException("El ID del entrenador no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(request.nombre))
            {
                throw new BadRequestException("El nombre del plan de entrenamiento es obligatorio.");
            }
            if (request.nombre.Length > 100)
            {
                throw new BadRequestException("El nombre del plan de entrenamiento no puede superar los 100 caracteres.");
            }
            if (request.sesionesEntrenamiento == null || request.sesionesEntrenamiento.Count == 0)
            {
                throw new BadRequestException("El plan de entrenamiento debe contener al menos una sesión de entrenamiento.");
            }
            foreach (var ts in request.sesionesEntrenamiento)
            {
                await _trainingSessionValidator.ValidateCreate(ts);
            }
        }
    }
}
