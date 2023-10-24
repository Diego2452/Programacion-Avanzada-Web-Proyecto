using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class ApiServiceUsuario
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiServiceUsuario(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<(List<Usuarios> usuarios, string message)> GetUsuariosAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "Usuarios";

            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}{apiEndpoint}");

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                List<Usuarios> usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jsonContent);
                return (usuarios, "Solicitud exitosa");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return (null, "No se encontraron usuarios.");
            }
            else
            {
                return (null, "Error al obtener usuarios desde la API.");
            }
        }

    }
}
