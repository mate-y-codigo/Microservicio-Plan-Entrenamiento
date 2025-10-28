using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.MuscleGroup
{
    public interface IMuscleGroupQuery<T>
    {
        Task<T> GetAll();
    }
}
