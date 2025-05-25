namespace CostManagementSystem.DTOs
{
    public class CostEntryDTO
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
