using Newtonsoft.Json;
using ProyectoProgramacionAvanzadaWeb.Models;
using System.Net;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb.Services
{
    public class TipoFinanciamientosApiService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public TipoFinanciamientosApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:baseUrl"];
        }

        public async Task<(List<TipoFinanciamientos> TipoFinanciamientos, string Message)> ObtenerTipoFinanciamientosAsync()
        {
            string apiEndpoint = "TipoFinanciamientos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<TipoFinanciamientos> tipoFinanciamientos = JsonConvert.DeserializeObject<List<TipoFinanciamientos>>(jsonContent);

                        return (tipoFinanciamientos, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return (null, "No se encontraron tipos de financiamientos en la base de datos.");
                    }
                    else
                    {
                        return (null, "Error al obtener tipos de financiamientos desde la API.");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> CrearTipoFinanciamientoAsync(TipoFinanciamientos tipoFinanciamiento)
        {
            string apiEndpoint = "TipoFinanciamientos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoFinanciamiento);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de financiamiento ha sido creado.");
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
                        return (false, $"Error al crear el tipo de financiamiento. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EliminarTipoFinanciamientoAsync(int id)
        {
            string apiEndpoint = $"TipoFinanciamientos/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Tipo de financiamiento eliminado con éxito.");
                    }
                    else
                    {
                        return (false, $"Error al eliminar el tipo de financiamiento. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al eliminar el tipo de financiamiento: {ex.Message}");
                }
            }
        }

        public async Task<(TipoFinanciamientos TipoFinanciamiento, string Message)> ObtenerDetallesTipoFinanciamientoAsync(int id)
        {
            string apiEndpoint = $"TipoFinanciamientos/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}{apiEndpoint}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        TipoFinanciamientos tipoFinanciamiento = JsonConvert.DeserializeObject<TipoFinanciamientos>(jsonContent);

                        if (tipoFinanciamiento == null)
                        {
                            return (null, "Tipo de financiamiento no encontrado en la API.");
                        }
                        else
                        {
                            return (tipoFinanciamiento, "Tipo de financiamiento cargado exitosamente desde la API.");
                        }
                    }
                    else
                    {
                        return (null, $"Error al obtener el tipo de financiamiento desde la API. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (null, $"Error al conectarse al API: {ex.Message}");
                }
            }
        }

        public async Task<(bool Success, string Message)> EditarTipoFinanciamientoAsync(TipoFinanciamientos tipoFinanciamiento)
        {
            if (tipoFinanciamiento.IdFinanciamiento == 0)
            {
                return (false, "El ID del tipo de financiamiento no puede ser 0.");
            }

            string apiEndpoint = $"TipoFinanciamientos/{tipoFinanciamiento.IdFinanciamiento}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(tipoFinanciamiento);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{apiEndpoint}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return (true, "Operación exitosa: El tipo de financiamiento ha sido modificado.");
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
                        return (false, $"Error al actualizar el tipo de financiamiento. Código de estado: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Error interno del servidor al actualizar el tipo de financiamiento: {ex.Message}");
                }
            }
        }
    }

}
