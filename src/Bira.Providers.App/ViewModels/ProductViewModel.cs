using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Bira.Providers.App.Extensions;

namespace Bira.Providers.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O campo Fornecedor é obrigatório")]
        public Guid ProviderId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo Descrição precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Currency]
        [DisplayName("Valor")]
        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        public decimal Value { get; set; }

        [DisplayName("Data de Criação")]
        [ScaffoldColumn(false)]
        public DateTime DateRegistration { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        [DisplayName("Fornecedor")]
        public ProviderViewModel Provider { get; set; }

        public IEnumerable<ProviderViewModel> Providers { get; set; }
    }
}
