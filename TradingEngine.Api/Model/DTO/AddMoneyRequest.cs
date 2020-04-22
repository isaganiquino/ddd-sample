using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradingEngine.Api.Model.DTO
{
    public class AddMoneyRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
