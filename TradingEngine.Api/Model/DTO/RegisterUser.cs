using System.ComponentModel.DataAnnotations;

namespace TradingEngine.Api.Model.DTO
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
