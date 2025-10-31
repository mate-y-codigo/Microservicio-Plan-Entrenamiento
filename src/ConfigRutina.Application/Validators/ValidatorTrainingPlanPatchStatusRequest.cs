using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class ValidatorTrainingPlanPatchStatusRequest : IValidatorTrainingPlanPatchStatusRequest
    {
        private readonly ITrainingPlanQuery _trainingPlanQuery;


        public ValidatorTrainingPlanPatchStatusRequest(ITrainingPlanQuery trainingPlanQuery)
        {
            _trainingPlanQuery = trainingPlanQuery;
        }

        public async Task Validate(string? strId)
        {
            Guid id;

            // id
            if (!Guid.TryParse(strId, out id))
                throw new BadRequestException(ExceptionMessage.TrainingPlanIdInvalidFormat);

            // training plan
            if (!(await _trainingPlanQuery.ExistsTrainingPlan(id)))
                throw new NotFoundException(ExceptionMessage.TrainingPlanInvalid);
        }
    }
}
