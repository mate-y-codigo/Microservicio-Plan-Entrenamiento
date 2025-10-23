using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.CustomExceptions
{
    public class ConflictException : Exception
    {
        public int Status { get; }

        public ConflictException(string msg) : base(msg)
        {
            Status = 409;
        }
    }
}
