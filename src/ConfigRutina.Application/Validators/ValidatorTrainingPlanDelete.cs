using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConfigRutina.Application.Validators
{
    public class ValidatorTrainingPlanDelete : IValidateTrainingPlanDelete
    {
        private readonly ITrainingPlanQuery _TrainingPlanQuery;

        public ValidatorTrainingPlanDelete(ITrainingPlanQuery trainingPlanQuery)
        {
            _TrainingPlanQuery = trainingPlanQuery;
        }

        public async Task Validate(string id)
        {
            Guid ID;
            if (!Guid.TryParse(id, out ID)){
                throw new BadRequestException(ExceptionMessage.TrainingPlanInvalidIdFormat);
            }

            var query = await _TrainingPlanQuery.GetTrainingPlanById(ID);

            if (query == null) {
                throw new NotFoundException("El plan de entrenamiento no existe");
            }
        }
    }
}
