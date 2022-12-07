using Bira.Providers.Business.Models;

namespace Bira.Providers.Business.Interfaces.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductPerProvider(Guid providerId);
        Task<IEnumerable<Product>> GetProductProvider();
        Task<Product> GetProductProvider(Guid id);
    }
}

