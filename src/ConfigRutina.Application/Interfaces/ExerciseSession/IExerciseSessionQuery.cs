using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.ExcerciseSession
{
    public interface IExerciseSessionQuery
    {
        Task<EjercicioSesion> GetById(Guid id);
        Task<List<EjercicioSesion>> GetExerciseSessionsByTrainingSession(Guid idTS);
    }
}
