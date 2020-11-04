using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BLL.Domain
{
    public class ProductsBLL
    {
        private readonly DataContext _context;

        public ProductsBLL(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
