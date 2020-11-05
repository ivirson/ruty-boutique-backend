using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BLL.Domain;
using Store.Models.Audit;

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
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetLog(DateTime? initialDate, DateTime? finalDate)
        {
            var log = await _errorLogBLL.GetErrorLog(initialDate, finalDate);
            return log;
        }
    }
}
