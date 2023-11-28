using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Identificaciones
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<TipoIdentificaciones> TipoIdentificaciones { get; set; } = new List<TipoIdentificaciones>();

        public async Task OnGetAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipoidentificaciones";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<TipoIdentificaciones> tipoIdentificaciones = JsonConvert.DeserializeObject<List<TipoIdentificaciones>>(jsonContent);

                        TipoIdentificaciones = tipoIdentificaciones;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Message = "No se encontraron Tipos de Identificacion en la base de datos.";
                    }
                    else
                    {
                        Message = "Error al obtener Tipos de Identificaciones desde la API.";
                    }
                }
                catch (Exception ex)
                {
                    Message = $"Error interno del servidor: {ex.Message}";
                }
            }
        }
    }
}
