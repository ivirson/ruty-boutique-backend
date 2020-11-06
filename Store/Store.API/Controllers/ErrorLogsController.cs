using System;
using Microsoft.AspNetCore.Mvc;
using Store.BLL.Audit;

namespace Store.API.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        private readonly ErrorLogBLL _errorLogBLL;

        public ErrorLogsController(ErrorLogBLL errorLogBLL)
        {
            _errorLogBLL = errorLogBLL;
        }

        // GET: api/log
        /// <summary>
        /// Gets the Error Log from database
        /// </summary>
        /// <returns>Returns the Error Log list</returns>
        [HttpGet]
        public IActionResult GetLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = _errorLogBLL.GetErrorLog(initialDate, finalDate);
            return Ok(log);
        }
    }
}
