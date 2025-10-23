using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Validators
{
    public interface IValidatorExerciseSearchRequest
    {
        Task Validate(string name, string mainMuscle, string muscleGroup, int category);
    }
}
