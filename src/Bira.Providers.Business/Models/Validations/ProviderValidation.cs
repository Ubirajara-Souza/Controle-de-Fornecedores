using Bira.Providers.Business.Models.Validations.Documents;
using FluentValidation;


namespace Bira.Providers.Business.Models.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TypeProviders == TypeProviders.pessoaFisica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidation.SizeCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.TypeProviders == TypeProviders.pessoaJuridica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.SizeCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                     .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}
