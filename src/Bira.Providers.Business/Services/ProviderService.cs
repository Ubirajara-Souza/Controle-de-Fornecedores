using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Interfaces.IServices;
using Bira.Providers.Business.Models;
using Bira.Providers.Business.Models.Validations;

namespace Bira.Providers.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Provider provider)
        {
            if (!RunValidation(new ProviderValidation(), provider)
                || !RunValidation(new AddressValidation(), provider.Address)) return;

            if (_providerRepository.Search(f => f.Document == provider.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!RunValidation(new ProviderValidation(), provider)) return;

            if (_providerRepository.Search(f => f.Document == provider.Document && f.Id != provider.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _providerRepository.Update(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!RunValidation(new AddressValidation(), address)) return;
            await _addressRepository.Update(address);
        }

        public async Task Delete(Guid id)
        {
            if (_providerRepository.GetProviderProductAddress(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }

            var address = await _addressRepository.GetAddressPerProvider(id);

            if (address != null)
            {
                await _addressRepository.Delete(address.Id);
            }

            await _providerRepository.Delete(id);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
