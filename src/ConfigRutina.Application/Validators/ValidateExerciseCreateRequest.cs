using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.Muscle;
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
        private readonly IMuscleQuery<Musculo> _muscleQuery;

        public ValidatorExerciseCreateRequest(
            IExcerciseQuery<Ejercicio> exerciseQuery,
            ICategoryExcerciseQuery<List<CategoriaEjercicio>> categoryExcerciseQuery,
            IMuscleQuery<Musculo> muscleQuery)
        {
            _exerciseQuery = exerciseQuery ?? throw new ArgumentNullException(nameof(exerciseQuery));
            _muscleQuery = muscleQuery ?? throw new ArgumentNullException(nameof(muscleQuery));
            _categoryExcerciseQuery = categoryExcerciseQuery ?? throw new ArgumentNullException(nameof(categoryExcerciseQuery));
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

            // muscle
            if (er.musculo < 1 || (er.musculo > (await _muscleQuery.GetCount())))
                throw new BadRequestException(ExceptionMessage.MuscleInvalid);

            // category
            if (er.categoriaEjercicio < 1 || (er.categoriaEjercicio > (await _categoryExcerciseQuery.GetCount())))
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
