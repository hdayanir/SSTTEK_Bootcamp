using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using WebApplication2.Context;
using static WebApplication2.Core.BasicECommerceCore;

namespace WebApplication2.Application
{
    public class BasicECommerceApplication : ControllerBase
    {
        public interface IUserService
        {
            Domain.User GetUserById(int userId);
            bool IsUserActive(int userId);
        }

        public class ProductService
        {
            private readonly IRepository<Domain.Product> _productRepository;
            private readonly IRepository<Domain.Category> _categoryRepository;
            public ProductService(  IRepository<Domain.Product> productRepository,
                                    IRepository<Domain.Category> categoryRepository)
            {
                _productRepository = productRepository;
                _categoryRepository = categoryRepository;
            }

            public List<Domain.Product> GetProductsByCategoryId(int categoryId)
            {
                var category = _categoryRepository.GetById(categoryId);
                if (category == null)
                {
                    throw new NotFoundException("Kategori bulunamadý");
                }
                var productsInCategory = _productRepository.GetProductsByCategoryId(categoryId);
                if (productsInCategory == null || !productsInCategory.Any())
                {
                    throw new NotFoundException("Bu kategoride ürün bulunamadý");
                }
                return productsInCategory;
            }
        }
    }
}