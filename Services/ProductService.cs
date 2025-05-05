using Dunnhumby.RestApi.Interface.Repositiries;
using Dunnhumby.RestApi.Interface.Services;
using Dunnhumby.RestApi.Models;

namespace Dunnhumby.RestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Product?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<Product> AddAsync(Product product)
        {
            // Impliment Enforcing Example for business logic: enforce SKU format
            if (!product.SKU.StartsWith("SKU-"))
            {
                product.SKU = "SKU-" + product.SKU;
            }

            return _repository.AddAsync(product);
        }

        public Task<bool> UpdateAsync(int id, ProductDto updatedProduct) => _repository.UpdateAsync(id, updatedProduct);

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
