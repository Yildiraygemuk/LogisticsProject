namespace Logistics.Entity
{
    public class OrderVm
    {
        public int OrderNo { get; set; }
        public string FromAddress { get; set; } = string.Empty;
        public string GoingAddress { get; set; } = string.Empty;
        public int Count { get; set; }
        public byte UnitQuantities { get; set; }
        public decimal Weight { get; set; }
        public byte UnitWeigh { get; set; }
        public int ProductCode { get; set; }
        public virtual ProductVm Product { get; set; }
        public string Note { get; set; } = string.Empty;
        public byte Status { get; set; }
    }
}
