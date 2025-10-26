using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class TrainingSessionValidator
    {
        private readonly ExerciseSessionValidator _exerciseSessionValidator;
        public TrainingSessionValidator(ExerciseSessionValidator exerciseSessionValidator)
        {
            _exerciseSessionValidator = exerciseSessionValidator;
        }

        public async Task ValidateCreate(TrainingSessionCreateRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException("La sesión de entrenamiento no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(request.nombre))
            {
                throw new BadRequestException("El nombre de la sesión es obligatorio.");
            }
            if (request.nombre.Length > 100)
            {
                throw new BadRequestException("El nombre de la sesión no puede superar los 100 caracteres.");
            }
            if (request.orden <= 0)
            {
                throw new BadRequestException("El orden de la sesión debe ser mayor a 0.");
            }
            if (request.sessionExerciseCreateRequests != null)
            {
                foreach (var ejercicio in request.sessionExerciseCreateRequests)
                {
                    _exerciseSessionValidator.ValidateCreate(ejercicio);
                }
            }
        }
    }
}
