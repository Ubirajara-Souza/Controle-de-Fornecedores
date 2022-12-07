using System.ComponentModel.DataAnnotations;

namespace Bira.Providers.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
