using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
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
    public class ValidatorExerciseSearchRequest : IValidatorExerciseSearchRequest
    {
        private readonly ICategoryExcerciseQuery<List<CategoriaEjercicio>> _categoryExcerciseQuery;
        private readonly IMuscleQuery<Musculo> _muscleQuery;

        public ValidatorExerciseSearchRequest(
            ICategoryExcerciseQuery<List<CategoriaEjercicio>> categoryExcerciseQuery,
            IMuscleQuery<Musculo> muscleQuery)
        {
            _categoryExcerciseQuery = categoryExcerciseQuery ?? throw new ArgumentNullException(nameof(categoryExcerciseQuery));
            _muscleQuery = muscleQuery ?? throw new ArgumentNullException(nameof(muscleQuery));
        }

        public async Task Validate(string? name, int muscle, int category)
        {
            // name
            if (name != null && name.Length > 100)
                throw new BadRequestException(ExceptionMessage.ExerciseNameLength);

            // muscle
            Console.WriteLine(await _muscleQuery.GetCount());
            if ((muscle > 0) && (muscle < 1 || (muscle > (await _muscleQuery.GetCount()))))
                throw new BadRequestException(ExceptionMessage.ExerciseCategoryInvalid);

            // category
            if ((category > 0) && (category < 1 || (category > (await _categoryExcerciseQuery.GetCount()))))
                throw new BadRequestException(ExceptionMessage.ExerciseCategoryInvalid);
        }
    }
}
