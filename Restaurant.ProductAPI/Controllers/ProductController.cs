using Microsoft.AspNetCore.Mvc;
using Restaurant.ProductAPI.Models;
using Restaurant.ProductAPI.Repository.Interface;

namespace Restaurant.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _responseDto = new ResponseDto();            
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                _responseDto.Result = await _productRepository.GetProducts();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in retrieving all products";
                _logger.LogError(ex, errorMessage);
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { errorMessage };
            }
            return _responseDto;
        }

        [HttpGet]
        [Route("productId")]
        public async Task<object> Get(int productId)
        {
            try
            {
                _responseDto.Result = await _productRepository.GetProductById(productId);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error in products with ID - {productId}";
                _logger.LogError(ex, errorMessage);
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { errorMessage };
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<object> Create([FromBody]ProductDto productDto)
        {
            try
            {
                _responseDto.Result = await _productRepository.CreateUpdateProduct(productDto);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error in adding product";
                _logger.LogError(ex, errorMessage);
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { errorMessage };
            }
            return _responseDto.Result;
        }

        [HttpPut]
        public async Task<object> Update(ProductDto productDto)
        {
            try
            {
                _responseDto.Result = await _productRepository.CreateUpdateProduct(productDto);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error in updating product";
                _logger.LogError(ex, errorMessage);
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { errorMessage };
            }
            return _responseDto.Result;
        }

        [HttpDelete]
        [Route("productId")]
        public async Task<object> Delete(int productId)
        {
            try
            {
                _responseDto.Result = await _productRepository.DeleteProductById(productId);
                if((bool)_responseDto.Result)
                    _responseDto.DisplayMessage = $"Product {productId} deleted successfully";
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error while deleting product with ID - {productId}";
                _logger.LogError(ex, errorMessage);
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { errorMessage };
            }
            return _responseDto;
        }
    }
}
