using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
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
    public class ValidatorExerciseCreateRequest : IValidatorExerciseCreateRequest
    {
        private readonly IExcerciseQuery<Ejercicio> _exerciseQuery;
        private readonly ICategoryExcerciseQuery<List<CategoriaEjercicio>> _categoryExcerciseQuery;

        public ValidatorExerciseCreateRequest(
            IExcerciseQuery<Ejercicio> exerciseQuery,
            ICategoryExcerciseQuery<List<CategoriaEjercicio>> categoryExcerciseQuery)
        {
            _exerciseQuery = exerciseQuery;
            _categoryExcerciseQuery = categoryExcerciseQuery;
        }

        public async Task Validate(ExerciseCreateRequest er)
        {
            Uri uriResult;

            // name
            if (string.IsNullOrWhiteSpace(er.nombre))
                throw new BadRequestException(ExceptionMessage.ExerciseNameRequired);

            if (er.nombre.Length > 100)
                throw new BadRequestException(ExceptionMessage.ExerciseNameLength);

            if (await _exerciseQuery.ExistsByName(er.nombre))
                throw new ConflictException(ExceptionMessage.ExerciseNameExist);

            // main muscle
            if (string.IsNullOrWhiteSpace(er.musculoPrincipal))
                throw new BadRequestException(ExceptionMessage.ExerciseMainMuscleRequired);

            if (er.musculoPrincipal.Length > 50)
                throw new BadRequestException(ExceptionMessage.ExerciseMainMuscleLength);

            // muscle Group
            if (string.IsNullOrWhiteSpace(er.grupoMuscular))
                throw new BadRequestException(ExceptionMessage.ExerciseMuscleGroupRequired);

            if (er.grupoMuscular.Length > 50)
                throw new BadRequestException(ExceptionMessage.ExerciseMuscleGroupLength);


            // category
            if (er.categoriaEjercicio < 1 || (er.categoriaEjercicio > (await _categoryExcerciseQuery.GetAll()).Count))
                throw new BadRequestException(ExceptionMessage.ExerciseCategoryInvalid);

            // url
            if (string.IsNullOrWhiteSpace(er.urlDemo))
                return;

            if (!Uri.TryCreate(er.urlDemo, UriKind.Absolute, out uriResult!))
                throw new BadRequestException(ExceptionMessage.ExerciseUrlDemoInvalid);

            if (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                throw new BadRequestException(ExceptionMessage.ExerciseUrlDemoInvalid);
        }
    }
}
