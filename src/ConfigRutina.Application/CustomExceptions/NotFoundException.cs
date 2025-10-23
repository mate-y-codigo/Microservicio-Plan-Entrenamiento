using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public int Status { get; }

        public NotFoundException(string msg) : base(msg)
        {
            Status = 404;
        }
    }
}
