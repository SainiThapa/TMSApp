using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMSApp.ViewModels;
using Xamarin.Essentials;

namespace TMSApp.Services
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;
        public AdminService()
        {
            _httpClient = new HttpClient() 
            {
                BaseAddress = new Uri("http://192.168.18.8:5000/api/")
            };
        }
        public async Task<bool> AdminLoginAsync(string email, string password)
        {
            var loginUrl = "http://192.168.18.8:5000/api/AccountApi/admin/login";

            // Create the data object
            var loginData = new
            {
                Email = email,
                Password = password
            };

            // Serialize the data to JSON
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request
                var response = await _httpClient.PostAsync(loginUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Read response content and deserialize JSON
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

                    // Save token to secure storage
                    if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                    {
                        await SecureStorage.SetAsync("jwt_token", tokenResponse.Token);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Admin Login Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<UserViewModel>> GetUserListAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("Token is missing.");

                var request = new HttpRequestMessage(HttpMethod.Get, "AccountApi/AspNetUsers");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserViewModel>>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDetailsViewModel> GetUserDetailsAsync(string userId)
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("Token is missing.");

                var request = new HttpRequestMessage(HttpMethod.Get, $"AccountApi/AspNetUsers/{userId}/details");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserDetailsViewModel>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteTasksAsync(string userId, List<int> taskIds)
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                Console.WriteLine(token.ToString());
                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("Token is missing.");

                var request = new HttpRequestMessage(HttpMethod.Post, $"AccountApi/AspNetUsers/{userId}/deleteTasks")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(taskIds), Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ResetUserPasswordAsync(string email, string newPassword)
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");
                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("Token is missing.");
                var requestPayload = new
                {
                    Email = email,
                    Password = newPassword
                };
                var request = new HttpRequestMessage(HttpMethod.Put, $"AccountApi/AspNetUser/{email}/updatePassword")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { Password = newPassword }), Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error resetting password: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<byte[]> GetUserSummaryFileAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is missing.");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.18.8:5000/api/AccountApi/Reports/UserSummary");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            return null;
        }

        public async Task<byte[]> GetTaskReportFileAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is missing.");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.18.8:5000/api/AccountApi/Reports/TaskReport");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            return null;
        }

        private async Task<string> SaveToFileAsync(HttpContent content, string fileName)
        {
            var fileBytes = await content.ReadAsByteArrayAsync();
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            File.WriteAllBytes(filePath, fileBytes);
            return filePath;
        }



    }
}
