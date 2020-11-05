using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.API.ViewModels;
using Store.BLL.Domain;
using Store.Models.Domain;

namespace Store.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsBLL _productsBLL;
        private readonly IMapper _mapper;

        public ProductsController(ProductsBLL productsBLL, IMapper mapper)
        {
            _productsBLL = productsBLL;
            _mapper = mapper;
        }

        // GET: api/Products
        /// <summary>
        /// Gets the Product list from database
        /// </summary>
        /// <returns>Returns the Product list</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetProducts()
        {
            var products = await _productsBLL.GetProducts();
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);

            foreach (var item in productsViewModel)
            {
                item.Categories = _mapper.Map<List<CategoryViewModel>>(item.Categories);
                item.Sizes = _mapper.Map<List<ProductSizeViewModel>>(item.Sizes);
            }
            return productsViewModel;
        }

        // GET: api/Products/5
        /// <summary>
        /// Get an specific Product from an Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Return an specific Product with Id equals to id parameter</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsBLL.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        /// <summary>
        /// Updating Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _productsBLL.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Products
        /// <summary>
        /// Create a new Product into a database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Return the created product</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _productsBLL.CreateProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Logic deletion of a product (set as inactive)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the inactivated product</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productsBLL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productsBLL.DeleteProduct(product);

            return product;
        }

        private bool ProductExists(int id)
        {
            return _productsBLL.GetProductById(id) != null ? true : false;
        }
    }
}
