using ConfigRutina.Application.DTOs.Request.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Validators
{
    public interface IValidatorExerciseCreateRequest
    {
        Task Validate(ExerciseCreateRequest er);
    }
}
