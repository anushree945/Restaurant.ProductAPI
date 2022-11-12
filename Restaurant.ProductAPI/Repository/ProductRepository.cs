using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.ProductAPI.DbContexts;
using Restaurant.ProductAPI.Models;
using Restaurant.ProductAPI.Repository.Interface;

namespace Restaurant.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            var result = product.ProductId > 0 ?
                _context.Products.Update(product) :
                _context.Products.Add(product);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductById(int productId)
        {
            try
            {
                Product product = await _context.Products
                    .Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                if (product == null)
                    return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _context.Products
                .Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
