using ConfigRutina.Application.DTOs.Response.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingSession
{
    public interface ITrainingSessionService
    {
        Task<TrainingSessionResponse> GetTrainingSessionById(string id);
    }
}
