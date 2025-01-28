using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {   
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAsync<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAsync<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJsonAsync(BasePath, model);
            if (response.IsSuccessStatusCode) 
            {
                return await response.ReadContentAsync<ProductModel>();
            } 
            else 
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJsonAsync(BasePath, model);
            if (response.IsSuccessStatusCode) 
            {
                return await response.ReadContentAsync<ProductModel>();
            } 
            else 
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
                //return await response.ReadContentAsync<bool>();
            }
            else 
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }
        }
    }
}