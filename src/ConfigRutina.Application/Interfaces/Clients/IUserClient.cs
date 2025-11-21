using ConfigRutina.Application.DTOs.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Clients
{
    public interface IUserClient
    {
        void SetAuthToken(string token);
        Task<UserResponse> ObtenerUsuario(Guid id, CancellationToken ct = default);
    }
}
