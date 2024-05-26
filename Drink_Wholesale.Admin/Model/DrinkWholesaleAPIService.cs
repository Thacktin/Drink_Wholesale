using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Drink_Wholesale.Desktop.Model;
using Drink_Wholesale.DTO;
using Microsoft.AspNetCore.WebUtilities;

namespace Drink_Wholesale.Admin.Model
{
    public class DrinkWholesaleAPIService
    {
        private readonly HttpClient _client;

        public DrinkWholesaleAPIService(string baseAddress)
        {
            _client = new() { BaseAddress = new Uri(baseAddress) };
        }

        #region Category

        public async Task<IEnumerable<CategoryDto>> LoadCategoriesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Category/");
            var res = response.IsSuccessStatusCode;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CategoryDto>>();
            }

            throw new NetworkException("Network error: " + response.StatusCode);
        }

        #endregion

        public async Task<IEnumerable<SubCategoryDto>> LoadSubCategoriesAsync(int categoryId)
        {
            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/SubCategory/", "categoryId", categoryId.ToString()));
            var res = response.IsSuccessStatusCode;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<SubCategoryDto>>();
            }

            throw new NetworkException("Network error: " + response.StatusCode);
        }

        public async Task CreateSubCategoryAsync(SubCategoryDto subCategory)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/SubCategory/", subCategory);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
            subCategory.Id = (await response.Content.ReadAsAsync<SubCategoryDto>()).Id;
        }

        public async Task<IEnumerable<ProductDto>> LoadProductsAsync(int subCategoryId)
        {

            HttpResponseMessage response = await _client.GetAsync(
                QueryHelpers.AddQueryString("api/product/", "subCatId", subCategoryId.ToString()));
            var res = response.IsSuccessStatusCode;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ProductDto>>();
            }

            throw new NetworkException("Network error: " + response.StatusCode);
        }


        public async Task CreateProductAsync(ProductDto productDto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/product/", productDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
            productDto.Id = (await response.Content.ReadAsAsync<ProductDto>()).Id;
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/product/{productDto.Id}", productDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response " + response);
            }
        }

        public async Task<IEnumerable<OrderDto>> LoadOrdersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Order");
            var res = response.IsSuccessStatusCode;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderDto>>();
            }

            throw new NetworkException("Network error: " + response.StatusCode);
        }

        public async Task<IEnumerable<CartItemDto>> LoadCartAsync(int orderId)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/order/{orderId}");

            if (response.IsSuccessStatusCode)
            { 
                var order = await response.Content.ReadAsAsync<OrderDto>();
                return order.Products; 
            }
            throw new NetworkException("Network error: " + response.StatusCode);
        }
    }
}
