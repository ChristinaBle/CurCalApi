using System;
using System.Collections.Generic;

namespace CurCalApi.Models
{
    public partial class ExchangeRates
    {
        public int Id { get; set; }
        public string BasicCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
