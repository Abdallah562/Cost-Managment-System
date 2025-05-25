namespace CostManagementSystem.DTOs
{
    public class InvoiceReminderDTO
    {
        public int InvoiceId { get; set; }
        public DateTime DueDate { get; set; }
        public string ClientEmail { get; set; }
    }
}
