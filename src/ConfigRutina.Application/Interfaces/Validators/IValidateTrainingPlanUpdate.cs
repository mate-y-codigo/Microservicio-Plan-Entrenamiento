using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Validators
{
    public interface IValidateTrainingPlanUpdate
    {
        Task validate(string id, UpdateTrainingPlanRequest request,bool IsAsigned);
    }
}
