using ConfigRutina.Application.CustomExceptions;
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
using System.Xml.Linq;

namespace ConfigRutina.Application.Validators
{
    public class ValidatorMuscleSearchRequest : IValidatorMuscleSearchRequest
    {
        public void Validate(int idMuscle, int idMuscleGroup, string? muscle, string? muscleGroup)
        {
            // muscle
            if ((idMuscle > 0) && (idMuscle < 1 || (idMuscle > 83)))
                throw new BadRequestException(ExceptionMessage.MuscleInvalid);

            if(muscle != null && muscle.Length > 50)
                throw new BadRequestException(ExceptionMessage.MuscleNameLength);

            // muscle group
            if ((idMuscleGroup > 0) && (idMuscleGroup < 1 || (idMuscleGroup > 15)))
                throw new BadRequestException(ExceptionMessage.MuscleGroupInvalid);

            if (muscleGroup != null && muscleGroup.Length > 50)
                throw new BadRequestException(ExceptionMessage.MuscleGroupNameLength);
        }
    }
}
