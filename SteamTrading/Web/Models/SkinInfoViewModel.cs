namespace Web.Models
{
    public class SkinInfoViewModel
    {
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        public string? Type { get; set; }
        public string? ItemCategory { get; set; }
        public int MarketRestriction { get; set; }
        public bool Tradable { get; set; }
        public bool Marketable { get; set; }
    }
}
