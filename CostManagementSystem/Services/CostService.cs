using CostManagementSystem.Data;
using CostManagementSystem.Models;
using CostManagementSystem.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace CostManagementSystem.Services
{
    public class CostService : ICostService
    {
        private readonly ApplicationDbContext _context;

        public CostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CostEntry> Add(CostEntry entry)
        {
            await _context.AddAsync(entry);
            _context.SaveChanges();

            return entry;
        }

        public async Task<IEnumerable<CostEntry>> GetAll()
        {
            return await _context.CostEntries.ToListAsync();
        }
    }
}
