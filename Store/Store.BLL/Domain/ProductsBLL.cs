using Microsoft.EntityFrameworkCore;
using Store.Data;
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

        public ProductsBLL(DataContext context, ErrorLogBLL errorLogBLL)
        {
            _context = context;
            _errorLogBLL = errorLogBLL;
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
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
            
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
        }

        public async Task DeleteProduct(Product product)
        {
            try
            {
                product.SetInactive();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
        }
    }
}
