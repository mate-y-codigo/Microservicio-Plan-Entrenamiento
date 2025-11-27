using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.ExerciseSession
{
    public interface IExerciseSessionService
    {
        Task<EjercicioSesion> CreateAsync(Guid planId, SessionExerciseCreateRequest request);
        Task<ExerciseSessionResponse> GetExerciseSessionById(Guid id);
        
    }
}
