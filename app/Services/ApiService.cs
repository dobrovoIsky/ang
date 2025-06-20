using System.Net.Http.Json;
using System.Threading.Tasks;
using NutritionApp.Models;

namespace NutritionApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        // Переконайтеся, що тут вказана правильна адреса вашого опублікованого сервера
        private const string BaseUrl = "https://bjuapiserver.azurewebsites.net";

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<AuthResponse> RegisterUserAsync(object payload)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", payload);

            if (response.IsSuccessStatusCode)
            {
                // Якщо успіх (код 200), читаємо JSON
                return await response.Content.ReadFromJsonAsync<AuthResponse>();
            }

            // Якщо помилка, читаємо тіло як звичайний текст
            var errorContent = await response.Content.ReadAsStringAsync();
            return new AuthResponse { Message = errorContent, UserId = 0 };
        }

        public async Task<AuthResponse> LoginUserAsync(object payload)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", payload);

            if (response.IsSuccessStatusCode)
            {
                // Якщо успіх, читаємо JSON
                return await response.Content.ReadFromJsonAsync<AuthResponse>();
            }

            // Якщо помилка, читаємо тіло як звичайний текст
            var errorContent = await response.Content.ReadAsStringAsync();
            return new AuthResponse { Message = errorContent, UserId = 0 };
        }

        public async Task<UserProfile> GetUserProfileAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<UserProfile>($"/api/Profile/{userId}");
        }

        public async Task<UserProfile> UpdateUserProfileAsync(int userId, object payload)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Profile/{userId}", payload);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserProfile>();
            }
            return null;
        }

        public class MealPlanResponse
        {
            public string MealPlan { get; set; }
        }

        // Новий метод для генерації плану харчування
        public async Task<MealPlanResponse> GenerateMealPlanAsync(int userId)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Nutrition/generate-custom-plan",
                new { userId }); // Надсилаємо userId в тілі запиту

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MealPlanResponse>();
            }
            return null;
        }

        public async Task<List<MealPlan>> GetMealPlanHistoryAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<List<MealPlan>>($"/api/nutrition/history/{userId}");
        }
    }
    }