using Bira.Providers.Business.Models;

namespace Bira.Providers.Business.Interfaces.IServices
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}
