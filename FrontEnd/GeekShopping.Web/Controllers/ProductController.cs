using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }
        
        [Authorize]
        [HttpGet("Create")]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            
            return View(model);
        }

        [Authorize]
        [HttpGet("Update")]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();            
        }

        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            
            return View(model);
        }

        [Authorize]
        [HttpGet("Delete")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.FindProductById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();            
        }

        [HttpPost("Delete")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.DeleteProductById(model.Id);
            if (response)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            
            return View(model);
        }
    }
}