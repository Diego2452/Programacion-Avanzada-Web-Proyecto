using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class RolesApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public RolesApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<Roles> Roles, string Message)> ObtenerRolesAsync()
        {
            string apiEndpoint = "Roles";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<Roles> roles = JsonConvert.DeserializeObject<List<Roles>>(jsonContent);

                        return (roles, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron roles en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener roles desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearRolAsync(Roles rol)
        {
            string apiEndpoint = "Roles";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(rol);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El rol ha sido creado.");
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

                        return (false, errorMessageBuilder.ToString());
                    }
                    else
                    {
                        return (false, $"Error al crear el rol. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarRolAsync(int id)
        {
            string apiEndpoint = $"Roles/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Rol eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el rol. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el rol: {ex.Message}");
                }
            }
        }

        public async Task<(Roles Rol, string Message)> ObtenerDetallesRolAsync(int id)
        {
            string apiEndpoint = $"Roles/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        Roles rol = JsonConvert.DeserializeObject<Roles>(jsonContent);

                        if (rol == null)
                        {
                            return (null, "Rol no encontrado en la API.");
                        }
                        else
                        {
                            return (rol, "Rol cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el rol desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarRolAsync(Roles rol)
        {
            if (rol.IdRol == 0)
            {
                return (false, "El ID del rol no puede ser 0.");
            }

            string apiEndpoint = $"Roles/{rol.IdRol}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(rol);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El rol ha sido modificado.");
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

                        return (false, errorMessageBuilder.ToString());
                    }
                    else
                    {
                        return (false, $"Error al actualizar el rol. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el rol: {ex.Message}");
                }
            }
        }
    }

}
