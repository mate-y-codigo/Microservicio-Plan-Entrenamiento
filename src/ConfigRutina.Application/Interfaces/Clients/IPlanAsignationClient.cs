
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Clients
{
    public interface IPlanAsignationClient
    {
        Task<bool> response(Guid IdTrainingPlan, CancellationToken ct = default);
    }
}
