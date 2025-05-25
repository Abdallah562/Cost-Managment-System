namespace CostManagementSystem.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public float Tax { get; set; }
        public float Discount { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
