using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Muscle
{
    public interface IMuscleQuery<T>
    {
        Task<int> GetCount();
        Task<List<T>> GetAll();
        Task<T?> SearchById(int id);
        Task<List<T>> GetByFilter(int? idMuscle, int? idMuscleGroup, string? muscle, string? muscleGroup);
    }
}
