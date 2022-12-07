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
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IProductService productService,
            IProviderRepository providerRepository, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = productService;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            var product = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductProvider());

            return View(product);
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await FillProviders(new ProductViewModel());

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await FillProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            var imgPrefix = Guid.NewGuid() + "_";

            if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            var product = _mapper.Map<Product>(productViewModel);
            await _productService.Add(product);

            if (!ValidOperation()) return View(productViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Product", "Edit")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Edit")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            var productUpdate = await GetProduct(id);
            productViewModel.Provider = productUpdate.Provider;
            productViewModel.Image = productUpdate.Image;

            if (!ModelState.IsValid) return View(productViewModel);

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }
                productUpdate.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Value = productViewModel.Value;
            productUpdate.Active = productViewModel.Active;

            var product = _mapper.Map<Product>(productUpdate);
            await _productService.Update(product);

            if (!ValidOperation()) return View(productViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Product", "Delete")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [ClaimsAuthorize("Product", "Delete")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);
            if (product == null) return NotFound();

            await _productService.Delete(id);

            if (!ValidOperation()) return View(product);

            TempData["Success"] = "Produto excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return product;
        }

        private async Task<ProductViewModel> FillProviders(ProductViewModel product)
        {
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return product;
        }

        private async Task<bool> UploadFile(IFormFile file, string imgPrefix)
        {
            if (file == null) return false;
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
