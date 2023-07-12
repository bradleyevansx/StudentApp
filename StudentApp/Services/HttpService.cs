using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAPI.Domain.Models;

namespace StudentApp.Services;


    public class HttpService : IHttpService
    {
        private HttpClient _httpclient;

        public HttpService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        private void SetAuthenticationHeaders(string token)
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task PostNewPracticeSessionAsync(PracticeSession practiceSession)
        {
            var json = JsonSerializer.Serialize(practiceSession);


            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var response = await _httpclient.PostAsync("https://bepapi.azurewebsites.net/api/PracticeSession", content);
        }

        public async Task UpsertPracticeSessionAsync(PracticeSession practiceSession)
        {
            var json = JsonSerializer.Serialize(practiceSession);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpclient.PutAsync("https://bepapi.azurewebsites.net/api/PracticeSession", content);
        }


        public async Task<AuthenticationResponse?> RegisterUserAsync(UserInfo newUser)
        {
            var json = JsonSerializer.Serialize(newUser);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            using var response = await _httpclient.PostAsync("https://bepapi.azurewebsites.net/api/Auth/register", content);
            
            if (!response.IsSuccessStatusCode || response.StatusCode is not HttpStatusCode.OK)
            {
                return null;
            }
            
            var authenticationResponse = await JsonSerializer.DeserializeAsync<AuthenticationResponse>(await response.Content.ReadAsStreamAsync());

            SetAuthenticationHeaders(authenticationResponse.accessToken);
            
            return authenticationResponse;
        }

        public async Task<AuthenticationResponse?> TryAuthUserAsync(string refreshTokenId)
        {
            using var response = await _httpclient.GetAsync($"https://bepapi.azurewebsites.net/api/Auth/refresh?refreshTokenId={refreshTokenId}");

            if (!response.IsSuccessStatusCode || response.StatusCode is not HttpStatusCode.OK)
            {
                return null;
            }

            var authenticationResponse = await JsonSerializer.DeserializeAsync<AuthenticationResponse>(await response.Content.ReadAsStreamAsync());
            
            SetAuthenticationHeaders(authenticationResponse.accessToken);
            

            return authenticationResponse;
        }
        public async Task<AuthenticationResponse?> TryAuthUserAsync(UserInfo newUser)
        {
            var usernamePassword = new[] {newUser.Username, newUser.Password};
            
            var json = JsonSerializer.Serialize(usernamePassword);

            
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
            var response = await _httpclient.PostAsync("https://bepapi.azurewebsites.net/api/Auth/login", content);

            if (!response.IsSuccessStatusCode || response.StatusCode is not HttpStatusCode.OK)
            {
                return null;
            }

            var authenticationResponse = JsonSerializer.Deserialize<AuthenticationResponse>(await response.Content.ReadAsStreamAsync());
            
            SetAuthenticationHeaders(authenticationResponse.accessToken);

            return authenticationResponse;
        }

        public async Task<List<PracticeSession>?> GetAsync(string userId, string accessToken)
        {
            SetAuthenticationHeaders(accessToken);
            
            var response = await _httpclient.GetAsync("https://bepapi.azurewebsites.net/api/PracticeSession/self");

            if (response.StatusCode is HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
            
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var practiceSessions = JsonSerializer.Deserialize<List<PracticeSession>>(content, options);

                return practiceSessions;
            }

            return null;
        }

        


      
    }
