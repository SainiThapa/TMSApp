using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMSApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms; // Add Xamarin.Essentials for SecureStorage
namespace TMSApp.Services
{

public class AuthService
    {
        private readonly HttpClient _httpClient;
        
        public AuthService()
        {

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://192.168.18.8:5000/api/")
            };
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginUrl = "AccountApi/login";

            // Create the data object
            var loginData = new
            {
                Email = email,
                Password = password
            };

            // Serialize the data to JSON
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("Content PRadip: "+content);

            try
            {
                // Send POST request
                var response = await _httpClient.PostAsync(loginUrl, content);
                Console.WriteLine(response.ToString() + "Pradip");

                if (response.IsSuccessStatusCode)
                {

                    // Read response content and deserialize JSON
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

                    // Save token to secure storage
                    if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                    {
                        await SecureStorage.SetAsync("jwt_token", tokenResponse.Token);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception pradip: {ex.Message}");

                return false;
            }
        }

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            var registerUrl = "http://192.168.18.8:5000/api/AccountApi/register";

            // Create the data object based on RegisterViewModel
            var registerData = new
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            // Serialize the data to JSON
            var json = JsonConvert.SerializeObject(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request
                var response = await _httpClient.PostAsync(registerUrl, content);

                // Check if the registration was successful
                if (response.IsSuccessStatusCode)
                {
                    // Optionally read the response message
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Registration Response: " + responseBody);

                    return true; // Registration successful
                }
                else
                {
                    // Log error response
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Registration failed: " + errorResponse);

                    return false; // Registration failed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during registration: {ex.Message}");
                return false; // An error occurred
            }
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("jwt_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            Console.WriteLine(token);
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync("http://192.168.18.8:5000/api/AccountApi/logout", null);
            if (response.IsSuccessStatusCode)
            {
                // Clear token from secure storage upon successful logout
                SecureStorage.Remove("jwt_token");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Handle error
                Console.WriteLine("Logout failed.");
            }
        }
        public async Task<UserProfileViewModel> GetUserProfileAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                var profileUrl = "AccountApi/Profile";

                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("User is not authenticated.");
                var request = new HttpRequestMessage(HttpMethod.Get, profileUrl);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error retrieving profile: {response.StatusCode}");
                        return null;
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();

                    var profile = JsonConvert.DeserializeObject<UserProfileViewModel>(responseBody);
                    return profile;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception ===========: {ex.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

    }

    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

}