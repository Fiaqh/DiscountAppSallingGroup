namespace SallingApiJsonClass
{
    public class Offer
    {
        public string currency { get; set; }
        public double discount { get; set; }
        public string ean { get; set; }
        public DateTime endTime { get; set; }
        public DateTime lastUpdate { get; set; }
        public double newPrice { get; set; }
        public double originalPrice { get; set; }
        public double percentDiscount { get; set; }
        public DateTime startTime { get; set; }
        public double stock { get; set; }
        public string stockUnit { get; set; }
    }
}