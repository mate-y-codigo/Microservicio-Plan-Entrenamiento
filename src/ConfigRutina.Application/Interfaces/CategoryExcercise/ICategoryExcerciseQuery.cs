using ConfigRutina.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.CategoryExcercise
{
    public interface ICategoryExcerciseQuery<T>
    {
        Task<T> GetAll();
    }
}
