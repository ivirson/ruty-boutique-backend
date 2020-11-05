using Store.Models.Core;
using System;

namespace Store.Models.Audit
{
    public class ErrorLog
    {
        public ErrorLog(int userId, string message, string stackTrace, string calledMethod)
        {
            UserId = userId;
            Message = message;
            StackTrace = stackTrace;
            CalledMethod = calledMethod;
            Date = DateTime.Now;
        }

        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
        public string CalledMethod { get; private set; }
    }
}
