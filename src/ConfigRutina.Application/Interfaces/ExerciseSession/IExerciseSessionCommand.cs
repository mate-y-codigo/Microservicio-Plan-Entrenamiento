using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.ExcerciseSession
{
    public interface IExerciseSessionCommand
    {
        Task InsertExcerciseSession(EjercicioSesion es);
        Task UpdateExcerciseSession(EjercicioSesion es);

    }
}
