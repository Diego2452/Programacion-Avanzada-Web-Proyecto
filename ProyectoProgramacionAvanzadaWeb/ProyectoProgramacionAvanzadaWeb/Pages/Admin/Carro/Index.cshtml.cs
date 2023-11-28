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

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Carro
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<Carros> Carros { get;set; } = new List<Carros>();

        public async Task OnGetAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "Carros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Carros> carros = JsonConvert.DeserializeObject<List<Carros>>(jsonContent);

                        Carros = carros;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Message = "No se encontraron carros en la base de datos.";
                    }
                    else
                    {
                        Message = "Error al obtener carros desde la API.";
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
