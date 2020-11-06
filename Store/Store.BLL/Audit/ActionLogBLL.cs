using Store.Data;
using Store.Models.Audit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Store.BLL.Audit
{
    public class ActionLogBLL
    {
        private readonly DataContext _context;
        private readonly ErrorLogBLL _errorLogBLL;

        public ActionLogBLL(DataContext context, ErrorLogBLL errorLogBLL)
        {
            _context = context;
            _errorLogBLL = errorLogBLL;
        }

        public List<ActionLog> GetProductLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = new List<ActionLog>();
            if (finalDate == null)
            {
                finalDate = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            if (initialDate == null)
            {
                log = _context.ActionLogs.Where(e => e.Date <= finalDate).ToList();
            }
            else
            {
                log = _context.ActionLogs.Where(e => e.Date >= initialDate && e.Date <= finalDate).ToList();
            }

            return log;
        }

        public void CreateLogEvent(ActionLog log)
        {
            try
            {
                _context.ActionLogs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
        }
    }
}
