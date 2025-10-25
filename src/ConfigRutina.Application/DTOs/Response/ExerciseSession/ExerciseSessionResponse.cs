using ConfigRutina.Application.DTOs.Response.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.ExerciseSession
{
    public class ExerciseSessionResponse
    {
        public Guid id { get; set; }
        public int targetSets { get; set; }
        public int targetReps { get; set; }
        public float targetWeight { get; set; }
        public int rest { get; set; }
        public int order { get; set; }
        public ExerciseResponse exercise { get; set;}
    }
}
