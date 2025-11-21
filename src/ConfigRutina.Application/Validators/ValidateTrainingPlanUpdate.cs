using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class ValidateTrainingPlanUpdate : IValidateTrainingPlanUpdate
    {
        private readonly ITrainingPlanQuery _trainingPlanQuery;
        private readonly IExerciseSessionQuery _exerciseSessionQuery;
        private readonly ITrainingSessionQuery _trainingSessionQuery;

        public ValidateTrainingPlanUpdate(ITrainingPlanQuery trainingPlanQuery, IExerciseSessionQuery exerciseSessionQuery, ITrainingSessionQuery trainingSessionQuery)
        {
            _trainingPlanQuery = trainingPlanQuery;
            _exerciseSessionQuery = exerciseSessionQuery;
            _trainingSessionQuery = trainingSessionQuery;
        }

        public async Task validate(string id, UpdateTrainingPlanRequest request)
        {
            
            Guid ID;
            if (!Guid.TryParse(id, out ID))
            {
                throw new BadRequestException("El formato ingresado no es valido");
            }

            var plan = await _trainingPlanQuery.GetTrainingPlanById(ID);

            if (plan == null) {
                throw new NotFoundException("El plan de entrenamiento no existe");
            }

           
        }
    }
}
