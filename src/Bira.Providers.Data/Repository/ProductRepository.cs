using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Models;
using Bira.Providers.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bira.Providers.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext _context) : base(_context) { }
        public async Task<Product> GetProductProvider(Guid id)
        {
            return await Context.Products.AsNoTracking().Include(p => p.Provider)
                .FirstOrDefaultAsync(prod => prod.Id == id);

        }
        public async Task<IEnumerable<Product>> GetProductProvider()
        {
            return await Context.Products.AsNoTracking().Include(p => p.Provider)
                .OrderBy(prod => prod.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductPerProvider(Guid providerId)
        {
            return await Search(p => p.ProviderId == providerId);
        }
    }
}
