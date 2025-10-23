using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
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

        public ValidatorExerciseSearchRequest(ICategoryExcerciseQuery<List<CategoriaEjercicio>> categoryExcerciseQuery)
        {
            _categoryExcerciseQuery = categoryExcerciseQuery;
        }

        public async Task Validate(string name, string mainMuscle, string muscleGroup, int category)
        {
            // name
            if (name != null && name.Length > 100)
                throw new BadRequestException(ExceptionMessage.ExerciseNameLength);

            // main muscle
            if (mainMuscle != null && mainMuscle.Length > 50)
                throw new BadRequestException(ExceptionMessage.ExerciseMainMuscleLength);

            // group muscle
            if (muscleGroup != null && muscleGroup.Length > 50)
                throw new BadRequestException(ExceptionMessage.ExerciseMuscleGroupLength);

            // category
            if ((category > 0) && (category < 1 || (category > (await _categoryExcerciseQuery.GetAll()).Count)))
                throw new BadRequestException(ExceptionMessage.ExerciseCategoryInvalid);
        }
    }
}
