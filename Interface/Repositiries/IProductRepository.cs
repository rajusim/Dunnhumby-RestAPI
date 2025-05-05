using Dunnhumby.RestApi.Models;

namespace Dunnhumby.RestApi.Interface.Repositiries
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(int id, ProductDto product);
        Task<bool> DeleteAsync(int id);
    }

}

