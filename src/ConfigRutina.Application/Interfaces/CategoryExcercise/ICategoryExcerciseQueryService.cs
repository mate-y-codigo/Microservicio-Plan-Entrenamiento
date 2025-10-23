using ConfigRutina.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.CategoryExcercise
{
    public interface ICategoryExcerciseQueryService
    {
        Task<List<CategoryExerciseResponse>?> GetAll();
    }
}
