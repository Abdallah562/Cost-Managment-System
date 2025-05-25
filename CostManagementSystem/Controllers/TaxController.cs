using CostManagementSystem.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        [HttpPost("Calculate")]
        public IActionResult Calculate(TaxDTO dto)
        {
            decimal taxAmount = dto.Subtotal * (dto.TaxRate / 100);
            decimal totalWithTax = dto.Subtotal + taxAmount;

            return Ok(new {taxAmount,totalWithTax});
        }

    }
}
