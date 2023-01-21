using App.Web.Models;

namespace App.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<CategoryModel>>>("categories");
            return response.Data;
        }

    }
}