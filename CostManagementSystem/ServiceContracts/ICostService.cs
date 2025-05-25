using CostManagementSystem.Models;

namespace CostManagementSystem.ServiceContracts
{
    public interface ICostService
    {
        Task<CostEntry> Add(CostEntry entry);
        Task<IEnumerable<CostEntry>> GetAll();
    }
}
