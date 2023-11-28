using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoRoles
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Roles Roles { get; set; } = new Roles();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"roles/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Roles = JsonConvert.DeserializeObject<Roles>(jsonContent);

                        if (Roles == null)
                        {
                            Message = "Tipo de Rol no encontrado en la API.";
                        }
                        else
                        {
                            Message = "Tipo de Rol cargado exitosamente desde la API.";
                        }
                    }
                    else
                    {
                        Message = "Error al obtener el Tipo de Rol desde la API. Código de estado: " + (int)response.StatusCode;
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
