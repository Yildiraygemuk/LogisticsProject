using Logistics.Core.Entities.Concrete;

namespace Logistics.Entity
{
    public class Product: BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Order> Orders { get; set; }
    }
}
