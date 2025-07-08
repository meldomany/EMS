using EMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogHistoriesController : ControllerBase
    {
        private readonly ILogHistoryService logHistoryService;

        public LogHistoriesController(ILogHistoryService logHistoryService)
        {
            this.logHistoryService = logHistoryService;
        }

        [HttpGet]
        [Route("GetAllLogHistories")]
        public async Task<IActionResult> GetAllLogHistorys()
        {
            var logHistorys = await logHistoryService.GetAllLogHistorysAsync();
            return Ok(logHistorys);
        }

        [HttpGet]
        [Route("GetLogHistoryById/{id}")]
        public async Task<IActionResult> GetLogHistoryById(int id)
        {
            var logHistory = await logHistoryService.GetLogHistoryByIdAsync(id);
            if (logHistory == null)
            {
                return NotFound($"LogHistory with ID {id} not found.");
            }
            return Ok(logHistory);
        }

    }
}
