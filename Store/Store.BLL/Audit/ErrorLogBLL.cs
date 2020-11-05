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
    public class ErrorLogBLL
    {
        private readonly DataContext _context;

        public ErrorLogBLL(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ErrorLog>> GetErrorLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = new List<ErrorLog>();
            if (finalDate == null)
            {
                finalDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            if (initialDate == null)
            {
                log =  await _context.ErrorLogs.Where(e => e.Date <= finalDate).ToListAsync();
            }
            else
            {
                log = await _context.ErrorLogs.Where(e => e.Date >= initialDate && e.Date <= finalDate).ToListAsync();
            }
            return log;
        }

        public async Task CreateErrorLogEvent(Exception exception, MethodBase calledMethod)
        {
            var userId = 1;
            var error = new ErrorLog(userId, exception.Message, exception.StackTrace, calledMethod.DeclaringType.ReflectedType.FullName);
            _context.ErrorLogs.Add(error);
            await _context.SaveChangesAsync();
        }
    }
}
