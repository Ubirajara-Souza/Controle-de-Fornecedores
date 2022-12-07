using Bira.Providers.Business.Interfaces.IRepository;
using Bira.Providers.Business.Interfaces.IServices;
using Bira.Providers.Business.Notifications;
using Bira.Providers.Business.Services;
using Bira.Providers.Data.Context;
using Bira.Providers.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using static Bira.Providers.App.Extensions.CurrencyAttribute;

namespace Bira.Providers.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyValidationAttributeAdapterProvider>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
