using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.ExerciseSession
{
    public interface IExerciseSessionService
    {
        Task<ExerciseSessionResponse> GetExcerciseSessionById(string id);
    }
}
