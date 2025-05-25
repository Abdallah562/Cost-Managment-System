using CostManagementSystem.DTOs;
using CostManagementSystem.Models;
using CostManagementSystem.ServiceContracts;
using CostManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> Generate(InvoiceDTO dto)
        {
            var items = dto.Items;
            var invoiceItems = items.Select(item => new InvoiceItem
            {
                Name = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
            }).ToList();
            var invoice = new Invoice
            {
                ClientId = dto.ClientId,
                Discount = dto.Discount,
                Items = invoiceItems,
                Tax = dto.Tax
            };

            await _invoiceService.Add(invoice);

            return Ok(invoice);
        }
        [HttpPut("{invoiceId}/Edit")]
        public async Task<IActionResult> Edit(int invoiceId, [FromBody] List<InvoiceItemDTO> itemsDto)
        {
            var invoice = await _invoiceService.FindById(invoiceId);
            if (invoice == null)
                return NotFound($"Invoice with ID:{invoiceId} Not Found");

            List<InvoiceItem> items = itemsDto.Select(item => new InvoiceItem
            {
                Name = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
            }).ToList();

            invoice.Items = items;

            _invoiceService.Edit(invoice);
            return Ok(invoice);

        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetById(int invoiceId)
        {
            var invoice = await _invoiceService.FindById(invoiceId);

            if(invoice == null)
                return NotFound($"Invoice with ID:{invoiceId} Not Found");

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceService.GetAll();

            return Ok(invoices);
        }

        [HttpPost("SendReminder")]
        public async Task<IActionResult> SendReminder([FromBody] InvoiceReminderDTO dto)
        {
            InvoiceReminder invoiceReminder = new InvoiceReminder
            {
                ClientEmail = dto.ClientEmail,
                DueDate = dto.DueDate,
                InvoiceId = dto.InvoiceId
            };

            var result = await _invoiceService.SendEmailReminder(invoiceReminder);

            return Ok(result);
        }

    }
}
