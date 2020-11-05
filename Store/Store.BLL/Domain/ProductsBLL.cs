using Microsoft.EntityFrameworkCore;
using Store.BLL.Audit;
using Store.Data;
using Store.Models.Audit;
using Store.Models.Domain;
using Store.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Store.BLL.Domain
{
    public class ProductsBLL
    {
        private readonly DataContext _context;
        private readonly ErrorLogBLL _errorLogBLL;
        private readonly ProductLogBLL _productLogBLL;

        public ProductsBLL(DataContext context, ErrorLogBLL errorLogBLL, ProductLogBLL productLogBLL)
        {
            _context = context;
            _errorLogBLL = errorLogBLL;
            _productLogBLL = productLogBLL;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = null;
            try
            {
                products = await _context.Products
                    .Include(p => p.Categories)
                    .Include(p => p.Log)
                    .Include(p => p.Ratings)
                    .Include(p => p.Sizes)
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = null;
            try
            {
                product = await _context.Products
                    .Include(p => p.Categories)
                    .Include(p => p.Log)
                    .Include(p => p.Ratings)
                    .Include(p => p.Sizes)
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }

            return product;
        }

        public async Task CreateProduct(Product product)
        {
            var userId = 1;
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                await _productLogBLL.CreateProductLogEvent(
                    new ProductLog(product.Id, userId, LogTypeEnum.CREATE)
                );
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
            
        }

        public async Task UpdateProduct(Product product)
        {
            var userId = 1;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _productLogBLL.CreateProductLogEvent(
                    new ProductLog(product.Id, userId, LogTypeEnum.UPDATE)
                );
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
        }

        public async Task DeleteProduct(Product product)
        {
            var userId = 1;
            try
            {
                product.SetInactive();
                await _context.SaveChangesAsync();
                await _productLogBLL.CreateProductLogEvent(
                    new ProductLog(product.Id, userId, LogTypeEnum.DELETE)
                );
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
        }
    }
}
