using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SkinInfoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        [Display(Name = "Type")]
        public string? Type { get; set; }
        [Display(Name = "Category")]
        public string? ItemCategory { get; set; }
        public string? MarketHashName { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public int MarketRestriction { get; set; }
        public bool Tradable { get; set; }
        public bool Marketable { get; set; }
    }
}
