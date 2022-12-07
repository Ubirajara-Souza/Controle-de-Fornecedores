using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Interfaces.IServices;
using Bira.Providers.Business.Models;
using Bira.Providers.Business.Models.Validations;

namespace Bira.Providers.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }
        public async Task Add(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;
            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;
            await _productRepository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
