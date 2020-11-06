using Store.Data;
using Store.Models.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Store.BLL.Audit
{
    public class ErrorLogBLL
    {
        private readonly DataContext _context;

        public ErrorLogBLL(DataContext context)
        {
            _context = context;
        }

        public List<ErrorLog> GetErrorLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = new List<ErrorLog>();
            if (finalDate == null)
            {
                finalDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            if (initialDate == null)
            {
                log = _context.ErrorLogs.Where(e => e.Date <= finalDate).ToList();
            }
            else
            {
                log = _context.ErrorLogs.Where(e => e.Date >= initialDate && e.Date <= finalDate).ToList();
            }
            return log;
        }

        public void CreateErrorLogEvent(Exception exception, MethodBase calledMethod)
        {
            var userId = 1;
            var error = new ErrorLog(userId, exception.Message, exception.StackTrace, String.Concat(calledMethod.DeclaringType.FullName, ".", calledMethod.Name));
            _context.ErrorLogs.Add(error);
            _context.SaveChanges();
        }
    }
}
