using Bira.Providers.Business.Models;

namespace Bira.Providers.Business.Interfaces.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressPerProvider(Guid providerId);
    }
}
