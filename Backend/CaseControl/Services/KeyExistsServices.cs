using CaseControl.Api.Interfaces;

namespace CaseControl.Api.Services
{
    public class KeyExistsServices : IKeyExistsServices
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        public KeyExistsServices(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> KeyExists(string key)//Recibe la Key enviada desde el UserController.
        {
            var url = "http://s460-aud04:8080/security/session_exists/";//Contiene la URL del servidor
            System.Net.Http.HttpResponseMessage response = await _httpClient.GetAsync(url + key);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.Contains("session_exists\":true");
        }
    }
}
