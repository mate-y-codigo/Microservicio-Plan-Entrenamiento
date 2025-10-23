using ConfigRutina.Application.DTOs.Request;
using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Excercise
{
    public interface IExcerciseCommand
    {
        Task Insert(Ejercicio exercise);
        Task Update(Ejercicio exercise);
        Task Delete(Ejercicio exercise);
    }
}
