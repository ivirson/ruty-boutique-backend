using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.API.InputModels;
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
        public IActionResult GetProducts()
        {
            var products = _productsBLL.GetProducts();

            var productsViewModel = products.ToList()
                .Select(p => new ProductViewModel(p.Name, p.Description, p.Price, p.Color, p.ProductCode)
                {
                    Sizes = _mapper.Map<List<ProductSize>, List<ProductSizeViewModel>>(p.Sizes),
                    Qty = p.Sizes.Sum(s => s.Qty),
                    Rating = p.Ratings.Count,
                    Categories = _mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(p.Categories),
                });

            return Ok(productsViewModel);
        }

        // GET: api/Products/5
        /// <summary>
        /// Get an specific Product from an Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Return an specific Product with Id equals to id parameter</returns>
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productsBLL.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<Product, ProductViewModel>(product);

            return Ok(productViewModel);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Updating Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productInputModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, ProductInputModel productInputModel)
        {
            if (id != productInputModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var product = _mapper.Map<ProductInputModel, Product>(productInputModel);
                _productsBLL.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_productsBLL.ProductExists(id))
                {
                    return NotFound();
                }
            }

            return CreatedAtAction("GetProduct", new { id = productInputModel.Id }, productInputModel);
        }

        // POST: api/Products
        /// <summary>
        /// Create a new Product into a database
        /// </summary>
        /// <param name="productInputModel"></param>
        /// <returns>Return the created product</returns>
        [HttpPost]
        public IActionResult PostProduct(ProductInputModel productInputModel)
        {
            var product = _mapper.Map<ProductInputModel, Product>(productInputModel);
            _productsBLL.CreateProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Logic deletion of a product (set as inactive)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productsBLL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productsBLL.DeleteProduct(product);

            return Ok();
        }
    }
}
