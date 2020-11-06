using Microsoft.EntityFrameworkCore;
using Store.BLL.Audit;
using Store.Data;
using Store.Models.Audit;
using Store.Models.Domain;
using Store.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Store.BLL.Domain
{
    public class ProductsBLL
    {
        private readonly DataContext _context;
        private readonly ErrorLogBLL _errorLogBLL;
        private readonly ActionLogBLL _actionLogBLL;
        private readonly CategoryBLL _categoryBLL;

        public ProductsBLL(DataContext context, ErrorLogBLL errorLogBLL, ActionLogBLL actionLogBLL, CategoryBLL categoryBLL)
        {
            _context = context;
            _errorLogBLL = errorLogBLL;
            _actionLogBLL = actionLogBLL;
            _categoryBLL = categoryBLL;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = null;
            try
            {
                products = _context.Products
                    .Include(p => p.Categories)
                    .Include(p => p.Log)
                    .Include(p => p.Ratings)
                    .Include(p => p.Sizes)
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .ToList();

                products.ForEach(
                    p => p.Categories.ForEach(
                        c => c.Category = _categoryBLL.GetCategoryById(c.CategoryId)
                    )
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            try
            {
                product = _context.Products
                    .Include(p => p.Categories)
                    .Include(p => p.Log)
                    .Include(p => p.Ratings)
                    .Include(p => p.Sizes)
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .SingleOrDefault(p => p.Id == id);

                if (product != null)
                {
                    product.Categories.ForEach(
                        c => c.Category = _categoryBLL.GetCategoryById(c.CategoryId)
                    );
                }
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }

            return product;
        }

        public void CreateProduct(Product product)
        {
            var userId = 1;
            try
            {
                product.ProductCode = GenerateProductCode(product);
                _context.Products.Add(product);
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(product.Id, userId, LogTypeEnum.CREATE, EntitiesEnum.PRODUCT)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
            
        }

        public void UpdateProduct(Product product)
        {
            var userId = 1;
            //var previousProduct = GetProductById(product.Id);
            //previousProduct = 
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(product.Id, userId, LogTypeEnum.UPDATE, EntitiesEnum.PRODUCT)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
        }

        public void DeleteProduct(Product product)
        {
            var userId = 1;
            try
            {
                product.SetInactive();
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(product.Id, userId, LogTypeEnum.DELETE, EntitiesEnum.PRODUCT)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
        }

        public string GenerateProductCode(Product product)
        {
            var str = "";
            try
            {
                var random = new Random();
                str = string.Concat(
                    _categoryBLL.GetCategoryById(product.Categories.FirstOrDefault().CategoryId).Name.Substring(0, 3).ToUpper(),
                    DateTime.Now.Year,
                    random.Next(1000000, 9999999)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
            return str;
        }

        public bool ProductExists(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return product != null;
        }
    }
}
