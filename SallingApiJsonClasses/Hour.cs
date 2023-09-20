namespace SallingApiJsonClass
{
    public class Hour
    {
        public DateTime close { get; set; }
        public bool closed { get; set; }
        public DateTime date { get; set; }
        public DateTime open { get; set; }
        public string type { get; set; }
        public List<double> customerFlow { get; set; }
    }
}