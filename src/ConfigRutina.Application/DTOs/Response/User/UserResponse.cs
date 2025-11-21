using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.User
{
    public class UserResponse
    {
            [Required]
            public Guid Id { get; set; }
            [Required]
            public string Nombre { get; set; }
            [Required]
            public string Apellido { get; set; }
            [Required]
            public int RolId { get; set; }
        
    }
}
