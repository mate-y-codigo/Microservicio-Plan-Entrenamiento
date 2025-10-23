using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConfigRutina.Application.CustomExceptions
{
    public class ApiError
    {
        [JsonPropertyName("message")]
        public string? message {get; set;}
    }
}
