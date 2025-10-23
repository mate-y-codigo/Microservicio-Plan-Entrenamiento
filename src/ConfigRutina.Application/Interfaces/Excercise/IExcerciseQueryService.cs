using ConfigRutina.Application.DTOs.Response.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Excercise
{
    public interface IExcerciseQueryService
    {
        Task<List<ExerciseResponse>> Search(string name, string mainMuscle, string muscleGroup, int category, bool active);
        Task<ExerciseResponse> SearchById(string strId);
    }
}
