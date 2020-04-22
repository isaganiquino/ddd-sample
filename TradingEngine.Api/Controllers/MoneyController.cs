using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingEngine.Api.Domain.Monies;
using TradingEngine.Api.Domain.User;
using TradingEngine.Api.Model.DTO;

namespace TradingEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;

        public MoneyController(ICurrencyService currencyService, IUserService userService)
        {
            _userService = userService;
            _currencyService = currencyService;
        }

        // GET: api/trading/balance
        [Route("balance")]
        [HttpGet]
        public async Task<IActionResult> Balance(string username)
        {
            try
            {
                var user = await _userService.GetUserAsync(username);
                return Ok(user.Balance.GetAllMonies());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Route("store")]
        [HttpPost]
        public async Task<IActionResult> Store([FromBody] AddMoneyRequest request)
        {
            try
            {
                var currency = await _currencyService.GetCurrencyAsync(request.CurrencyId);
                var money = new Money() { Currency = currency, Amount = request.Amount };
                var user = await _userService.GetUserAsync(request.Username);

                user.Balance.AddMoney(money);
                var result = await _userService.UpdateUserBalance(user);

                return Ok(result.Balance.GetAllMonies());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Route("exchange")]
        [HttpPost]
        public async Task<IActionResult> Exchange([FromBody] ExchangeMoneyRequest request)
        {
            try
            {
                var fromCurrency = await _currencyService.GetCurrencyAsync(request.FromCurrencyId);
                var toCurrency = await _currencyService.GetCurrencyAsync(request.ToCurrencyId);
                var money = new Money() { Currency = fromCurrency, Amount = request.Amount };
                var user = await _userService.GetUserAsync(request.Username);

                user.Balance.Exchange(money, toCurrency);
                var result = await _userService.UpdateUserBalance(user);

                return Ok(result.Balance.GetAllMonies());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SendMoneyRequest request)
        {
            try
            {
                var currency = await _currencyService.GetCurrencyAsync(request.CurrencyId);
                var money = new Money() { Currency = currency, Amount = request.Amount };
                var fromUser = await _userService.GetUserAsync(request.FromUsername);
                var toUser = await _userService.GetUserAsync(request.ToUsername);

                fromUser.Balance.ChargeMoney(money);
                toUser.Balance.AddMoney(money);
                
                await _userService.UpdateUserBalance(fromUser);
                await _userService.UpdateUserBalance(toUser);

                return Ok("Send money successful!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}