using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Validators
{
    public interface IValidatorMuscleSearchRequest
    {
        void Validate(int idMuscle, int idMuscleGroup, string? muscle, string? muscleGroup);
    }
}
