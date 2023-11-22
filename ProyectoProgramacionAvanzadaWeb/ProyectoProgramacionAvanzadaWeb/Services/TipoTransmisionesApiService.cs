using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class TipoTransmisionesApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public TipoTransmisionesApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<TipoTransmisiones> TipoTransmisiones, string Message)> ObtenerTipoTransmisionesAsync()
        {
            string apiEndpoint = "TipoTransmisiones";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<TipoTransmisiones> tipoTransmisiones = JsonConvert.DeserializeObject<List<TipoTransmisiones>>(jsonContent);

                        return (tipoTransmisiones, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron tipos de transmisiones en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener tipos de transmisiones desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearTipoTransmisionAsync(TipoTransmisiones tipoTransmision)
        {
            string apiEndpoint = "TipoTransmisiones";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoTransmision);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de transmisión ha sido creado.");
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
                        return (false, $"Error al crear el tipo de transmisión. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarTipoTransmisionAsync(int id)
        {
            string apiEndpoint = $"TipoTransmisiones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Tipo de transmisión eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el tipo de transmisión. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el tipo de transmisión: {ex.Message}");
                }
            }
        }

        public async Task<(TipoTransmisiones TipoTransmision, string Message)> ObtenerDetallesTipoTransmisionAsync(int id)
        {
            string apiEndpoint = $"TipoTransmisiones/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        TipoTransmisiones tipoTransmision = JsonConvert.DeserializeObject<TipoTransmisiones>(jsonContent);

                        if (tipoTransmision == null)
                        {
                            return (null, "Tipo de transmisión no encontrado en la API.");
                        }
                        else
                        {
                            return (tipoTransmision, "Tipo de transmisión cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el tipo de transmisión desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarTipoTransmisionAsync(TipoTransmisiones tipoTransmision)
        {
            if (tipoTransmision.IdTransmision == 0)
            {
                return (false, "El ID del tipo de transmisión no puede ser 0.");
            }

            string apiEndpoint = $"TipoTransmisiones/{tipoTransmision.IdTransmision}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoTransmision);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de transmisión ha sido modificado.");
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
                        return (false, $"Error al actualizar el tipo de transmisión. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el tipo de transmisión: {ex.Message}");
                }
            }
        }
    }

}
