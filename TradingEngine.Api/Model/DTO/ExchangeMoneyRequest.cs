using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Model.DTO
{
    public class ExchangeMoneyRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int FromCurrencyId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int ToCurrencyId { get; set; }
    }
}
