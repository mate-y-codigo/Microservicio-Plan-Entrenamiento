using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Excercise
{
    public interface IExcerciseQuery<T>
    {
        Task<T?> GetById(Guid id);
        Task<List<T>> GetByFilter(string name, string mainMuscle, string muscleGroup, int category, bool active);
        Task<bool> ExistsById(Guid id);
        Task<bool> ExistsByName(string name);
    }
}
