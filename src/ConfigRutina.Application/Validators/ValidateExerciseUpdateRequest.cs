using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Response;
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
    public class ValidateExerciseUpdateRequest : IValidateExerciseUpdateRequest
    {
        private readonly IExcerciseQuery<Ejercicio> _exerciseQuery;

        public ValidateExerciseUpdateRequest(IExcerciseQuery<Ejercicio> exerciseQuery)
        {
            _exerciseQuery = exerciseQuery;
        }

        public async Task Validate(string strId, ExerciseUpdateRequest eur)
        {
            Uri uriResult;
            Guid id;

            // id
            if (!Guid.TryParse(strId, out id))
                throw new BadRequestException(ExceptionMessage.ExerciseIdInvalidFormat);

            if (!await _exerciseQuery.ExistsById(id))
                throw new NotFoundException(ExceptionMessage.ExerciseIdUnknown);

            // name
            var exerciseCurrent = await _exerciseQuery.GetById(id);

            if (string.IsNullOrWhiteSpace(eur.nombre))
                throw new BadRequestException(ExceptionMessage.ExerciseNameRequired);

            if (eur.nombre.Length > 100)
                throw new BadRequestException(ExceptionMessage.ExerciseNameLength);

            if ((exerciseCurrent != null) && (exerciseCurrent.Nombre != eur.nombre) && (await _exerciseQuery.ExistsByName(eur.nombre)))
                throw new ConflictException(ExceptionMessage.ExerciseNameExist);

            // url
            if (string.IsNullOrWhiteSpace(eur.urlDemo))
                return;

            if (!Uri.TryCreate(eur.urlDemo, UriKind.Absolute, out uriResult!))
                throw new BadRequestException(ExceptionMessage.ExerciseUrlDemoInvalid);

            if (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                throw new BadRequestException(ExceptionMessage.ExerciseUrlDemoInvalid);
        }
    }
}
