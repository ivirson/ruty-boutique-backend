using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Store.BLL.Audit
{
    public class ProductLogBLL
    {
        private readonly DataContext _context;
        private readonly ErrorLogBLL _errorLogBLL;

        public ProductLogBLL(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductLog>> GetProductLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = new List<ProductLog>();
            if (finalDate == null)
            {
                finalDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            if (initialDate == null)
            {
                log =  await _context.ProductLogs.Where(e => e.Date <= finalDate).ToListAsync();
            }
            else
            {
                log = await _context.ProductLogs.Where(e => e.Date >= initialDate && e.Date <= finalDate).ToListAsync();
            }

            return log;
        }

        public async Task CreateProductLogEvent(ProductLog log)
        {
            try
            {
                _context.ProductLogs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogBLL.CreateErrorLogEvent(ex, MethodBase.GetCurrentMethod());
            }
        }
    }
}
