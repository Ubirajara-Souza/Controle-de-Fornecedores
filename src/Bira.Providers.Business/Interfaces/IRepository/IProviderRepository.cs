using Bira.Providers.Business.Models;

namespace Bira.Providers.Business.Interfaces.IRepository
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);
        Task<Provider> GetProviderProductAddress(Guid id);
    }
}
