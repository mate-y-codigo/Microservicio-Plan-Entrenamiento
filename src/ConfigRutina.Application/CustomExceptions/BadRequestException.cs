using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public int Status { get; }

        public BadRequestException(string msg) : base(msg)
        {
            Status = 400;
        }
    }
}
