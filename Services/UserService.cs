using BlazorAppU.Models;
using System.Net.Http.Headers;

namespace BlazorAppU.Services
{
    public class UserService
    {
        private const string _USERNAME = "admin";
        private const string _PASSWORD = "password123";
        private AuthenticationHeaderValue GetBasicAuthHeader()
        {
            var credentials = $"{_USERNAME}:{_PASSWORD}";
            var base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));
            return new AuthenticationHeaderValue("Basic", base64Credentials);
        }


        private readonly HttpClient _httpClient;
        private string _APIURL = "https://webapplicationapiu20251107000045-aacchubmbza8gfby.mexicocentral-01.azurewebsites.net/api/Registros?usarAPIA=true";
        // Propiedad para almacenar el mensaje de error para la UI
        public string ErrorMessage { get; private set; }

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync(bool isApiA = true)
        {
            if (!isApiA) {
                _APIURL = "https://webapplicationapiu20251107000045-aacchubmbza8gfby.mexicocentral-01.azurewebsites.net/api/Registros?usarAPIA=false";
            }

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = GetBasicAuthHeader();

                    // Realiza la llamada HTTP y espera la respuesta
                    var response = await _httpClient.GetAsync(_APIURL);

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserializa el JSON de la respuesta al modelo List<UserDto>
                        return await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Manejo de Error 404
                        ErrorMessage = $"Error 404: El recurso de usuarios no se encuentra en la API {(isApiA ? "A" : "B")}.";
                    }
                    else
                    {
                        // Manejo de otros errores HTTP (400, 500, etc.)
                        ErrorMessage = $"Error HTTP {(int)response.StatusCode}: No se pudo cargar la lista de usuarios.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Error de red (servidor no disponible, problemas de DNS, etc.)
                    ErrorMessage = "Error de red: No se pudo conectar con el servidor API. Verifique la URL o la conexión.";
                }
                catch
                {
                    ErrorMessage = "Ocurrió un error inesperado al procesar la respuesta.";
                }
            return new List<User>(); // Devuelve una lista vacía en caso de error
        }

        public async Task<bool> CreateUserAsync(User nuevoUsuario, bool isApiA = true)
        {
            if (!isApiA)
            {
                _APIURL = "https://webapplicationapiu20251107000045-aacchubmbza8gfby.mexicocentral-01.azurewebsites.net/api/Registros";
            }

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = GetBasicAuthHeader();

                var response = await _httpClient.PostAsJsonAsync(_APIURL, nuevoUsuario);

                if (response.IsSuccessStatusCode)
                {
                    ErrorMessage = string.Empty;
                    return true;
                }
                else
                {
                    ErrorMessage = $"Error HTTP {(int)response.StatusCode}: No se pudo crear el usuario.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "Error de red: No se pudo conectar con el servidor API.";
            }
            catch (Exception)
            {
                ErrorMessage = "Ocurrió un error inesperado al crear el usuario.";
            }

            return false;
        }


    }
}
