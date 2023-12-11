using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Carro
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet()
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Index");
            }

            try
            {
                await SelectLists();
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error interno del servidor: " + ex.Message;
                return Page();
            }
        }

        [BindProperty]
        public Carros Carros { get; set; } = new Carros();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SelectLists();
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "carros";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(Carros);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Operación exitosa: El carro ha sido creado.";
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

                        ViewData["ErrorMessage"] = errorMessageBuilder.ToString();
                        await SelectLists();
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Error al crear el carro. Código de estado: " + (int)response.StatusCode;
                        await SelectLists();
                    }
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error interno del servidor: " + ex.Message;
                    await SelectLists();
                }
            }

            return Page();
        }

        private async Task SelectLists()
        {
            ViewData["IdCombustible"] = new SelectList(await GetCombustiblesAsync(), "IdCombustible", "TipoCombustible");
            ViewData["IdFinanciamiento"] = new SelectList(await GetFinanciamientosAsync(), "IdFinanciamiento", "TipoFinanciamiento");
            ViewData["IdModelo"] = new SelectList(await GetModelosAsync(), "IdModelo", "Modelo");
            ViewData["IdTransmision"] = new SelectList(await GetTransmisionesAsync(), "IdTransmision", "TipoTransmision");
        }

        public async Task<List<TipoCombustibles>> GetCombustiblesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipocombustibles";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<TipoCombustibles> combustibles = JsonConvert.DeserializeObject<List<TipoCombustibles>>(content);
                    return combustibles;
                }
                return null;
            }
        }

        public async Task<List<TipoFinanciamientos>> GetFinanciamientosAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipofinanciamientos";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<TipoFinanciamientos> financiamientos = JsonConvert.DeserializeObject<List<TipoFinanciamientos>>(content);
                    return financiamientos;
                }
                return null;
            }
        }

        public async Task<List<ModelosCarros>> GetModelosAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "modeloscarros";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<ModelosCarros> modelos = JsonConvert.DeserializeObject<List<ModelosCarros>>(content);
                    return modelos;
                }
                return null;
            }
        }

        public async Task<List<TipoTransmisiones>> GetTransmisionesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipotransmisiones";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<TipoTransmisiones> transmisiones = JsonConvert.DeserializeObject<List<TipoTransmisiones>>(content);
                    return transmisiones;
                }
                return null;
            }
        }
    }
}
