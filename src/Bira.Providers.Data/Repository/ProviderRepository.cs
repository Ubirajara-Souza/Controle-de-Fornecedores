using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Models;
using Bira.Providers.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bira.Providers.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(AppDbContext _context) : base(_context) { }
        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await Context.Providers.AsNoTracking().Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider> GetProviderProductAddress(Guid id)
        {
            return await Context.Providers.AsNoTracking()
                .Include(p => p.Products)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
