namespace Logistics.Entity
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
