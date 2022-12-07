using Microsoft.AspNetCore.Mvc;
using Bira.Providers.App.ViewModels;
using AutoMapper;
using Bira.Providers.Business.Models;
using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using static Bira.Providers.App.Extensions.CustomAuthorization;

namespace Bira.Providers.App.Controllers
{
    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IProviderService providerService,
            IMapper mapper, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _providerService = providerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            var providerMapped = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return View(providerMapped);
        }

        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null) return NotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("novo-fornecedor")]
        [HttpPost]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Add(provider);

            if (!ValidOperation()) return View(providerViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductAddress(id);

            if (providerViewModel == null) return NotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Update(provider);

            if (!ValidOperation()) return View(await GetProviderProductAddress(id));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Provider", "Delete")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderProductAddress(id);

            if (providerViewModel == null) return NotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Delete")]
        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var provider = await GetProviderProductAddress(id);
            if (provider == null) return NotFound();

            await _providerService.Delete(id);

            if (!ValidOperation()) return View(provider);

            return RedirectToAction("Index");


        }

        [AllowAnonymous]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);
            if (provider == null) return NotFound();

            return PartialView("_DetailsAddress", provider);
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);
            if (provider == null) return NotFound();

            return PartialView("_UpdateAddress", new ProviderViewModel { Address = provider.Address });
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(ProviderViewModel providerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid) return PartialView("_UpdateAddress", providerViewModel);

            var address = _mapper.Map<Address>(providerViewModel.Address);

            await _providerService.UpdateAddress(address);

            if (!ValidOperation()) return PartialView("_UpdateAddress", providerViewModel);

            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });
            return Json(new { success = true, url });
        }
        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderProductAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductAddress(id));
        }
    }
}
