using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.TipoRoles
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<Roles> Roles { get;set; } = new List<Roles>();

        public async Task OnGetAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "roles";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Roles> roles = JsonConvert.DeserializeObject<List<Roles>>(jsonContent);

                        Roles = roles;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Message = "No se encontraron Tipos de Roles en la base de datos.";
                    }
                    else
                    {
                        Message = "Error al obtener Tipos de Roles desde la API.";
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
