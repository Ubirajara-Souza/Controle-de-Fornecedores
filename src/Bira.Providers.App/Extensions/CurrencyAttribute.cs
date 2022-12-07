using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Bira.Providers.App.Extensions
{
    public class CurrencyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var currency = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }

            return ValidationResult.Success;

        }

        public class CurrencyAttributeAdapter : AttributeAdapterBase<CurrencyAttribute>
        {

            public CurrencyAttributeAdapter(CurrencyAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
            {

            }
            public override void AddValidation(ClientModelValidationContext context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));

                MergeAttribute(context.Attributes, "data-val", "true");
                MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
                MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
            }
            public override string GetErrorMessage(ModelValidationContextBase validationContext)
            {
                return "Moeda em formato inválido";
            }
        }

        public class CurrencyValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
        {
            private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
            public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
            {
                if (attribute is CurrencyAttribute currencyAttribute)
                {
                    return new CurrencyAttributeAdapter(currencyAttribute, stringLocalizer);
                }

                return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);

            }
        }
    }
}