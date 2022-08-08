using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class SkinInfoModel
    {
        public string? Name { get; set; }
        public string IconUrl { get; set; } = "https://steamcommunity-a.akamaihd.net/economy/image/";
        public string? Type { get; set; }
        public string? ItemCategory { get; set; }   
        public int MarketRestriction { get; set; }
        public bool Tradable { get; set; }
        public bool Marketable { get; set; }
    }
}
