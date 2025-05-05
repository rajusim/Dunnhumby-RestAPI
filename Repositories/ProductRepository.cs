
using Dunnhumby.RestApi.Context;
using Dunnhumby.RestApi.Interface.Repositiries;
using Dunnhumby.RestApi.Models;
using Microsoft.EntityFrameworkCore;


namespace Dunnhumby.RestApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _context;

        public ProductRepository(ProductDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.DateAdded = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, ProductDto updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.ProductName = updatedProduct.ProductName;
            product.Category = updatedProduct.Category;
            product.ProductCode = updatedProduct.ProductCode;
            product.Price = updatedProduct.Price;
            product.SKU = updatedProduct.SKU;
            product.StockQuantity = updatedProduct.StockQuantity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
