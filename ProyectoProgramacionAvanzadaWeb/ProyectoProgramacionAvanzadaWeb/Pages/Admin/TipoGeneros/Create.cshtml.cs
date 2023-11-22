using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoGeneros
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genero Genero { get; set; } = new Genero();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "Generos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(Genero);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Operación exitosa: El Tipo de Genero ha sido creado.";
                        return RedirectToPage("./Create");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                        StringBuilder errorMessageBuilder = new StringBuilder();
                        foreach (var error in errorResponse.Errors)
                        {
                            errorMessageBuilder.AppendLine($"{error.Key}: {error.Value.Errors[0].ErrorMessage}");
                        }

                        Message = errorMessageBuilder.ToString();
                    }
                    else
                    {
                        Message = "Error al crear el Tipo de Genero. Código de estado: " + (int)response.StatusCode;
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error interno del servidor: " + ex.Message;
                }
            }

            return Page();
        }
    }
}
