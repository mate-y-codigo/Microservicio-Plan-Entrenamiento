using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.TrainingSession
{
    public class TrainingSessionService : ITrainingSessionService
    {
        public Task<TrainingSessionResponse> GetTrainingSessionById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
