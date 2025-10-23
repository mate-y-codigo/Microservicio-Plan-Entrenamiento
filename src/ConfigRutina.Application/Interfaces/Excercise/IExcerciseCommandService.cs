using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Response.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Excercise
{
    public interface IExcerciseCommandService
    {
        Task<ExerciseResponse> Create(ExerciseCreateRequest request);
        Task<ExerciseResponse> Update(string id, ExerciseUpdateRequest request);
        Task<ExerciseResponse> Delete(string strId);
    }
}
