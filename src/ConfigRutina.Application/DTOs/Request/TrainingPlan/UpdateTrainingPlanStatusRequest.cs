using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.TrainingPlan
{
    public class UpdateTrainingPlanStatusRequest
    {
        public required bool status { get; set; } = false;
    }
}
