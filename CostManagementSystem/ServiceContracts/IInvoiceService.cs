using CostManagementSystem.Models;

namespace CostManagementSystem.ServiceContracts
{
    public interface IInvoiceService
    {
        Task<Invoice> Add(Invoice invoice);
        Invoice Edit(Invoice invoice);
        Task<Invoice> FindById(int id);
        Task<List<Invoice>> GetAll();
        Task<object> SendEmailReminder(InvoiceReminder reminder);

    }
}
