namespace CostManagementSystem.Models
{
    public class InvoiceReminder
    {
        public int InvoiceId { get; set; }
        public DateTime DueDate { get; set; }
        public string ClientEmail { get; set; }
    }
}
