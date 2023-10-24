using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Models;

namespace ProyectoProgramacionAvanzadaWeb.Pages.Admin.Usuario
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
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
                            await SelectLists();
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = $"usuarios/{Usuarios.IdUsuario}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(Usuarios);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Operación exitosa: El usuario ha sido modificado.";
                        return RedirectToPage("./Edit", new { id = Usuarios.IdUsuario });
                        //return RedirectToPage("./Edit");
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
                        await SelectLists();
                    }
                    else
                    {
                        Message = "Error al actualizar el usuario. Código de estado: " + (int)response.StatusCode;
                        await SelectLists();
                    }
                }
                catch (Exception ex)
                {
                    Message = "Error interno del servidor al actualizar el usuario: " + ex.Message;
                    await SelectLists();
                }
            }

            return Page();
        }

        private async Task SelectLists()
        {
            ViewData["IdTipoIdentificacion"] = new SelectList(await GetTipoIdentificacionesAsync(), "IdIdentificacion", "TipoIdentificacion");
            ViewData["IdSexo"] = new SelectList(await GetSexosAsync(), "IdSexo", "TipoSexo");
            ViewData["IdRol"] = new SelectList(await GetRolesAsync(), "IdRol", "NombreRol");
        }
        public async Task<List<TipoIdentificaciones>> GetTipoIdentificacionesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "tipoidentificaciones";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<TipoIdentificaciones> tipoIdentificaciones = JsonConvert.DeserializeObject<List<TipoIdentificaciones>>(content);
                    return tipoIdentificaciones;
                }
                return null;
            }
        }
        public async Task<List<Sexo>> GetSexosAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "sexos";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<Sexo> sexos = JsonConvert.DeserializeObject<List<Sexo>>(content);
                    return sexos;
                }
                return null;
            }
        }
        public async Task<List<Roles>> GetRolesAsync()
        {
            string baseUrl = _configuration["ApiSettings:baseUrl"];
            string apiEndpoint = "roles";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}{apiEndpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List<Roles> roles = JsonConvert.DeserializeObject<List<Roles>>(content);
                    return roles;
                }
                return null;
            }
        }
    }
}
