using ConfigRutina.Application.DTOs.Request.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Validators
{
    public interface IValidateExerciseUpdateRequest
    {
        Task Validate(string strId, ExerciseUpdateRequest eur);
    }
}
