using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TiposGeneros
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Genero Genero { get; set; } = new Genero();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"Generos/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Genero = JsonConvert.DeserializeObject<Genero>(jsonContent);

                        if (Genero == null)
                        {
                            Message = "Tipo de Genero no encontrado en la API.";
                        }
                        else
                        {
                            Message = "Tipo de Genero cargado exitosamente desde la API.";
                        }
                    }
                    else
                    {
                        Message = "Error al obtener el Tipo de Genero desde la API. Código de estado: " + (int)response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error al conectarse al API: " + ex.Message;
                }
            }

            return Page();
        }
    }
}
