namespace CostManagementSystem.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public int InvoiceId { get; set; }
    }
}
