using App.Web.Models;

namespace App.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryModel>> GetProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductWithCategoryModel>>>("products/GetProductsWithCategory");

            return response.Data;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseModel<ProductModel>>($"products/{id}");
            return response.Data;


        }

        public async Task<ProductModel> SaveAsync(ProductModel newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("products", newProduct);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseModel<ProductModel>>();

            return responseBody.Data;


        }
        public async Task<bool> UpdateAsync(ProductModel newProduct)
        {
            var response= await _httpClient.PutAsJsonAsync("products", newProduct);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}