namespace Logistics.Entity
{
    public class StatuDto
    {
        public string OrderNo { get; set; } = string.Empty;
        public byte Status { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
