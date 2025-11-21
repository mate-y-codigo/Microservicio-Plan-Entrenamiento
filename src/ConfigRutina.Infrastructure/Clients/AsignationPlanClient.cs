using ConfigRutina.Application.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Clients
{
    public class PlanAsignationClient : IPlanAsignationClient
    {
        private readonly HttpClient _httpClient;

        public PlanAsignationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> response(Guid IdTrainingPlan, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/AlumnoPlan/check/{IdTrainingPlan}",ct);
            if (!response.IsSuccessStatusCode) {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new Exception("Error al llamar a AsignacionRutina API" + " " + errorBody);
            }
            return await response.Content.ReadFromJsonAsync<bool>(cancellationToken: ct);
        }
    }
}
