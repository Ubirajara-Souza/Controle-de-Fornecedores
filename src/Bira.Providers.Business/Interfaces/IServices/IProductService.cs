using Bira.Providers.Business.Models;

namespace Bira.Providers.Business.Interfaces.IServices
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
