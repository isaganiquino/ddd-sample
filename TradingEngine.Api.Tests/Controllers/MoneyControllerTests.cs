using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TradingEngine.Api.Controllers;
using TradingEngine.Api.Domain.Monies;
using TradingEngine.Api.Domain.User;
using TradingEngine.Api.Model.DTO;
using TradingEngine.Api.Model.GeneratedContext;
using User = TradingEngine.Api.Domain.User.User;

namespace TradingEngine.Api.Tests.Controllers
{
    [TestFixture]
    class MoneyControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ICurrencyService> _currencyServiceMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _currencyServiceMock = new Mock<ICurrencyService>();
        }

        [Test]
        public async Task WhenCheckingBalanceAndReturnIsNullOrError_ThenThrowArgumentException()
        {
            //Arrange
            var fixture = new Fixture();
            var username = fixture.Create<string>();
            var user = fixture.Create<User>();

            _userServiceMock.Setup(x => x.GetUserAsync(username)).ReturnsAsync(user);

            //Action
            var target = new MoneyController(_currencyServiceMock.Object, _userServiceMock.Object);
            var result = await target.Balance(username);

            //Assert
            var objectResult = result.As<ObjectResult>();

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task WhenStoringMoneyAndReturnIsNullOrError_ThenThrowArgumentException()
        {
            //Arrange
            var fixture = new Fixture();
            var request = fixture.Create<AddMoneyRequest>();
            var currency = fixture.Create<Currency>();
            var user = fixture.Create<User>();

            _currencyServiceMock.Setup(x => x.GetCurrencyAsync(request.CurrencyId)).ReturnsAsync(currency);
            _userServiceMock.Setup(x => x.GetUserAsync(request.Username)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.UpdateUserBalance(user)).ReturnsAsync(user);

            //Action
            var target = new MoneyController(_currencyServiceMock.Object, _userServiceMock.Object);
            var result = await target.Store(request);

            //Assert
            var objectResult = result.As<ObjectResult>();

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task WhenExchangingMoneyAndReturnIsNullOrError_ThenThrowArgumentException()
        {
            //Arrange
            var fixture = new Fixture();
            var fromCurrency = new Currency() { Id = 1, Name = "USD", Ratio = 1 };
            var toCurrency = new Currency() { Id = 2, Name = "PHP", Ratio = 50 };
            var money = new Money() { Currency = fromCurrency, Amount = 100 };
            var user = fixture.Create<User>();
            var request = new ExchangeMoneyRequest()
            {
                 Username = user.Username,
                 FromCurrencyId = fromCurrency.Id,
                 ToCurrencyId = toCurrency.Id,
                 Amount = 10
            };

            user.Balance.AddMoney(money);
            
            _currencyServiceMock.Setup(x => x.GetCurrencyAsync(fromCurrency.Id)).ReturnsAsync(fromCurrency);
            _currencyServiceMock.Setup(x => x.GetCurrencyAsync(toCurrency.Id)).ReturnsAsync(toCurrency);
            _userServiceMock.Setup(x => x.GetUserAsync(user.Username)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.UpdateUserBalance(user)).ReturnsAsync(user);

            //Action
            var target = new MoneyController(_currencyServiceMock.Object, _userServiceMock.Object);
            var result = await target.Exchange(request);

            //Assert
            var objectResult = result.As<ObjectResult>();

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public async Task WhenSendingMoneyAndReturnIsNullOrError_ThenThrowArgumentException()
        {
            //Arrange
            var fixture = new Fixture();
            var currency = new Currency() { Id = 1, Name = "USD", Ratio = 1 };
            var money = new Money() { Currency = currency, Amount = 100 };
            var fromUser = fixture.Create<User>();
            var toUser = fixture.Create<User>();

            var request = new SendMoneyRequest()
            {
                FromUsername = fromUser.Username,
                CurrencyId = currency.Id,
                Amount = 10,
                ToUsername = toUser.Username
            };

            fromUser.Balance.AddMoney(money);

            _currencyServiceMock.Setup(x => x.GetCurrencyAsync(currency.Id)).ReturnsAsync(currency);
            _userServiceMock.Setup(x => x.GetUserAsync(fromUser.Username)).ReturnsAsync(fromUser);
            _userServiceMock.Setup(x => x.GetUserAsync(toUser.Username)).ReturnsAsync(toUser);
            _userServiceMock.Setup(x => x.UpdateUserBalance(fromUser)).ReturnsAsync(fromUser);
            _userServiceMock.Setup(x => x.UpdateUserBalance(toUser)).ReturnsAsync(toUser);

            //Action
            var target = new MoneyController(_currencyServiceMock.Object, _userServiceMock.Object);
            var result = await target.Send(request);

            //Assert
            var objectResult = result.As<ObjectResult>();

            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
