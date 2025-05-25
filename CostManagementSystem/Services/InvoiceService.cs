using CostManagementSystem.Data;
using CostManagementSystem.Models;
using CostManagementSystem.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CostManagementSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IInvoiceService> _logger;

        public InvoiceService(ApplicationDbContext context, ILogger<IInvoiceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Invoice> Add(Invoice invoice)
        {
            await _context.AddAsync(invoice);
            _context.SaveChanges();

            return invoice;
        }

        public Invoice Edit(Invoice invoice)
        {
            _context.Update(invoice);
            _context.SaveChanges();

            return invoice;
        }

        public async Task<Invoice> FindById(int id)
        {
            Invoice invoice = await _context.Invoices
                .Include(inv => inv.Items).SingleOrDefaultAsync(inv => inv.Id == id);

            return invoice;
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _context.Invoices.Include(inv => inv.Items).ToListAsync();
        }

        public async Task<object> SendEmailReminder(InvoiceReminder reminder)
        {
            List<InvoiceItem> items = await _context.InvoiceItems
                .Where(item => item.InvoiceId == reminder.InvoiceId).ToListAsync();
            
            if (items.Count == 0)
                return "Invalid Invoice ID";

            var totalPrice = 0.0;

            foreach (InvoiceItem item in items)
            {
                totalPrice += item.Quantity * item.UnitPrice;
            }

            string type = CheckDate(reminder.DueDate);

            string subject = type == "upcoming"
                        ? $"Reminder: Invoice {reminder.InvoiceId} is due soon"
                        : $"Overdue Notice: Invoice {reminder.InvoiceId}";

            string body = type == "upcoming"
                ? $"Dear client, your invoice with Id : {reminder.InvoiceId} with a total price of {totalPrice} is due in 2 days on {reminder.DueDate:MMMM dd, yyyy}. Please make payment on time."
                : $"Dear client, your invoice with Id : {reminder.InvoiceId} with a total price of {totalPrice} was due on {reminder.DueDate:MMMM dd, yyyy} and is now overdue. Please make the payment as soon as possible.";

            return new
            {
                reminder.ClientEmail,
                Subject = subject,
                Body = body
            };
        }
        public string CheckDate(DateTime date)
        {
            DateTime today = DateTime.Today;

            if (today > date)
                return "overdue";
            else
                return "upcoming";
        }
    }
}
