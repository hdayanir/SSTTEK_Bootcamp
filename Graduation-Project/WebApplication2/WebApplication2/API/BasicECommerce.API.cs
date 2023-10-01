using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using static WebApplication2.Application.BasicECommerceApplication;

namespace WebApplication2.API
{
    [ApiController]
    [Route("[ApiController]")]
    public class BasicECommerceAPI : ControllerBase
    {
        private readonly ILogger<BasicECommerceAPI> _logger;

        public BasicECommerceAPI(ILogger<BasicECommerceAPI> logger)
        {
            _logger = logger;
        }

        [Route("api/users")]
        public class UserController : Controller
        {
            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }


            [HttpGet("{userId}")]
            public IActionResult GetUser(int userId)
            {
                var user = _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
        }

        [Route("api/products")]
        public class ProductController : Controller
        {
            private readonly ProductService _productService;

            public ProductController(ProductService productService)
            {
                _productService = productService;
            }

            [HttpGet("category/{categoryId}")]
            public IActionResult GetProductsByCategory(int categoryId)
            {
                var products = _productService.GetProductsByCategoryId(categoryId);
                return Ok(products);
            }
        }
    }
}