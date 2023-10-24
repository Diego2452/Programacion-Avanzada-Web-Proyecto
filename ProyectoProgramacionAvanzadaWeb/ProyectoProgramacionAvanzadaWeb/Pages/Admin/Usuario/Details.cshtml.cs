using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class DetailsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Usuarios Usuarios { get; set; } = new Usuarios();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Message = "ID no proporcionado.";
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"Usuarios/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Usuarios = JsonConvert.DeserializeObject<Usuarios>(jsonContent);

                        if (Usuarios == null)
                        {
                            Message = "Usuario no encontrado en la API.";
                        }
                        else
                        {
                            Message = "Usuario cargado exitosamente desde la API.";
                        }
                    }
                    else
                    {
                        Message = "Error al obtener el usuario desde la API. Código de estado: " + (int)response.StatusCode;
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
