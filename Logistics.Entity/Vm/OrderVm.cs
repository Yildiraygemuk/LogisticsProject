namespace Logistics.Entity
{
    public class OrderVm
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string GoingAddress { get; set; } = string.Empty;
        public int Count { get; set; }
        public byte UnitQuantities { get; set; }
        public string UnitQuantitiesValue { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public byte UnitWeigh { get; set; }
        public string UnitWeighValue { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public virtual ProductVm Product { get; set; }
        public string Note { get; set; } = string.Empty;
        public byte Status { get; set; }
        public string StatusValue { get; set; } = string.Empty;
    }
}
