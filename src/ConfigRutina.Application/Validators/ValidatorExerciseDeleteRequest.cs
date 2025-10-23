using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class ValidatorExerciseDeleteRequest : IValidatorExerciseDeleteRequest
    {
        private readonly IExcerciseQuery<Ejercicio> _excerciseQuery;

        public ValidatorExerciseDeleteRequest(IExcerciseQuery<Ejercicio> excerciseQuery)
        {
            _excerciseQuery = excerciseQuery;
        }

        public async Task<Guid> Validate(string strId)
        {
            Guid id;

            if (!Guid.TryParse(strId, out id))
                throw new NotFoundException(ExceptionMessage.ExerciseIdInvalidFormat);

            if (!await _excerciseQuery.ExistsById(id))
                throw new BadRequestException(ExceptionMessage.ExerciseIdUnknown);

            return id;
        }
    }
}
