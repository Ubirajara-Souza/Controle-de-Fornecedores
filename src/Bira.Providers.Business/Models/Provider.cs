
namespace Bira.Providers.Business.Models
{
    public class Provider : Entity
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public TypeProviders TypeProviders { get; set; }

        public Address Address { get; set; }

        public bool Active { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }

}
