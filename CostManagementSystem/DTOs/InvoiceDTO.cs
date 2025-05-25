using CostManagementSystem.Models;

namespace CostManagementSystem.DTOs
{
    public class InvoiceDTO
    {
        public int ClientId { get; set; }
        public float Tax { get; set; }
        public float Discount { get; set; }
        public List<InvoiceItemDTO> Items { get; set; }
    }
}
