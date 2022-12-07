using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Models;
using Bira.Providers.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bira.Providers.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext _context) : base(_context) { }
        public async Task<Address> GetAddressPerProvider(Guid providerId)
        {
            return await Context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProviderId == providerId);
        }
    }
}
