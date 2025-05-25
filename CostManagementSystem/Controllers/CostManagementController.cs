using CostManagementSystem.DTOs;
using CostManagementSystem.Models;
using CostManagementSystem.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostManagementController : ControllerBase
    {
        private readonly ICostService _costService;

        public CostManagementController(ICostService costService)
        {
            _costService = costService;
        }

        [HttpPost("AddEntry")]
        public async Task<IActionResult> AddEntryAsync(CostEntryDTO dto)
        {
            var entry = new CostEntry
            {
                Category = dto.Category,
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date
            };

            await _costService.Add(entry);

            return Ok(entry);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var entries = await _costService.GetAll();

            return Ok(entries);
        }
    }
}
