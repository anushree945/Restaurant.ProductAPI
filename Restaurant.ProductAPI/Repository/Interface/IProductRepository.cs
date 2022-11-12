using Restaurant.ProductAPI.Models;

namespace Restaurant.ProductAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProductById(int productId);
    }
}
