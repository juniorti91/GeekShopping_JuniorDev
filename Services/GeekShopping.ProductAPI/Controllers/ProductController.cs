using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Ultils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            try
            {
                var products = await _repository.FindAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            try
            {
                var product = await _repository.FindById(id);
                if (product.Id <= 0)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO vo)
        {
            try
            {
                var product = await _repository.Create(vo);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
        {
            try
            {
                var product = await _repository.Update(vo);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}