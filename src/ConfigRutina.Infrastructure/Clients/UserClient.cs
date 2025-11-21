using ConfigRutina.Application.DTOs.Response.User;
using ConfigRutina.Application.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Clients
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;
        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<UserResponse> ObtenerUsuario(Guid id, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"api/Usuarios/{id}", ct);
            if (!response.IsSuccessStatusCode) { 
            
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"Error al llamar a Usuarios API:{response.StatusCode}-{errorBody}");
                return null;
            }
            return await response.Content.ReadFromJsonAsync<UserResponse>(cancellationToken:ct);

        }


    }
}
