using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMSApp.Models;
using TMSApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace TMSApp.Services
{

    public class TaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://192.168.18.8:5000/api/")
            };
        }

        public async Task<List<TaskViewModel>> GetTaskAsync()
        {
            var token = await GetTokenAsync();
            var loginUrl = "TaskItem/List";
            var request = new HttpRequestMessage(HttpMethod.Get, loginUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<List<TaskViewModel>>(responseBody);
                    return tasks;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve tasks: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception pradip: {ex.Message}");
                return null;
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

        public async Task<bool> AddTaskAsync(TaskViewModel task)
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token not found.");
                return false;
            }


            var addTaskUrl = "TaskItem/create";

            var taskJson = JsonConvert.SerializeObject(task);
            var content = new StringContent(taskJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.PostAsync(addTaskUrl, content);
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add task: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskViewModel task)
        {
            var token = await GetTokenAsync();
            var requestUrl = $"http://192.168.18.8:5000/api/TaskItem/edit/{task.Id}";
            var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var token = await GetTokenAsync();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"http://192.168.18.8:5000/api/TaskItem/Delete/{taskId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}