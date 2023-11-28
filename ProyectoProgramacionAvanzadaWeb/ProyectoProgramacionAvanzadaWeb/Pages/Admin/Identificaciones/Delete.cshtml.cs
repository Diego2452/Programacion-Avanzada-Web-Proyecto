using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Identificaciones
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public TipoIdentificaciones TipoIdentificaciones { get; set; } = new TipoIdentificaciones();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"tipoIdentificaciones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        TipoIdentificaciones = JsonConvert.DeserializeObject<TipoIdentificaciones>(jsonContent);

                        if (TipoIdentificaciones == null)
                        {
                            Message = "Tipo de Identificacion no encontrado en la API.";
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        Message = "Error al obtener el Tipo Identificacion desde la API. Código de estado: " + (int)response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error al conectarse al API: " + ex.Message;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"tipoIdentificaciones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        Message = "Tipo Identificacion borrado con éxito.";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        Message = "Error al borrar el Tipo Identificacion. Código de estado: " + (int)response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error interno del servidor al borrar el Tipo Identificacion: " + ex.Message;
                }
            }

            return Page();
        }
    }
}
