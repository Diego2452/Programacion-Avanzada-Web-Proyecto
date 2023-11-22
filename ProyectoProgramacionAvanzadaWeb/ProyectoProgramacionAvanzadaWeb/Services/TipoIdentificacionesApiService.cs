using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class TipoIdentificacionesApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public TipoIdentificacionesApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<TipoIdentificaciones> TipoIdentificaciones, string Message)> ObtenerTipoIdentificacionesAsync()
        {
            string apiEndpoint = "TipoIdentificaciones";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<TipoIdentificaciones> tipoIdentificaciones = JsonConvert.DeserializeObject<List<TipoIdentificaciones>>(jsonContent);

                        return (tipoIdentificaciones, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron tipos de identificaciones en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener tipos de identificaciones desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearTipoIdentificacionAsync(TipoIdentificaciones tipoIdentificacion)
        {
            string apiEndpoint = "TipoIdentificaciones";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoIdentificacion);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de identificación ha sido creado.");
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
                        return (false, $"Error al crear el tipo de identificación. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarTipoIdentificacionAsync(int id)
        {
            string apiEndpoint = $"TipoIdentificaciones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Tipo de identificación eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el tipo de identificación. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el tipo de identificación: {ex.Message}");
                }
            }
        }

        public async Task<(TipoIdentificaciones TipoIdentificacion, string Message)> ObtenerDetallesTipoIdentificacionAsync(int id)
        {
            string apiEndpoint = $"TipoIdentificaciones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        TipoIdentificaciones tipoIdentificacion = JsonConvert.DeserializeObject<TipoIdentificaciones>(jsonContent);

                        if (tipoIdentificacion == null)
                        {
                            return (null, "Tipo de identificación no encontrado en la API.");
                        }
                        else
                        {
                            return (tipoIdentificacion, "Tipo de identificación cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el tipo de identificación desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarTipoIdentificacionAsync(TipoIdentificaciones tipoIdentificacion)
        {
            if (tipoIdentificacion.IdIdentificacion == 0)
            {
                return (false, "El ID del tipo de identificación no puede ser 0.");
            }

            string apiEndpoint = $"TipoIdentificaciones/{tipoIdentificacion.IdIdentificacion}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoIdentificacion);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de identificación ha sido modificado.");
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
                        return (false, $"Error al actualizar el tipo de identificación. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el tipo de identificación: {ex.Message}");
                }
            }
        }
    }


}
