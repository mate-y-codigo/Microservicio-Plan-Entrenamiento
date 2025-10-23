using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.ExerciseSession
{
    public class ExerciseSessionService : IExerciseSessionService
    {
        public Task<ExerciseSessionResponse> GetExcerciseSessionById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
