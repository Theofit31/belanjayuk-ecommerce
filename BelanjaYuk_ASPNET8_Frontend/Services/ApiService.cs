using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BelanjaYuk_ASPNET8_Frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(HttpClient client)
        {
            _client = client;
        }
        // GET: Ambil data dari API
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _client.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[API ERROR] GET {endpoint} -> {response.StatusCode}");
                    return default;
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API EXCEPTION] GET {endpoint} -> {ex.Message}");
                return default;
            }
        }

        // POST: Kirim data ke API
        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _client.PostAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API EXCEPTION] POST {endpoint} -> {ex.Message}");
                throw;
            }
        }

        // PUT: Update data di API
        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                return await _client.PutAsJsonAsync(endpoint, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API EXCEPTION] PUT {endpoint} -> {ex.Message}");
                throw;
            }
        }

        // DELETE: Hapus data di API
        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                return await _client.DeleteAsync(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API EXCEPTION] DELETE {endpoint} -> {ex.Message}");
                throw;
            }
        }
    }
}