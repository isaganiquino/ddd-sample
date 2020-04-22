using Moq;
using NUnit.Framework;
using TradingEngine.Api.Domain.Monies;
using TradingEngine.Api.Implementations;
using TradingEngine.Api.Model.GeneratedContext;
using FluentAssertions;
using System;

namespace TradingEngine.Api.Tests.Domain.User
{
    [TestFixture]
    class BalanceTests
    {
        private Currency _usd;
        private Currency _php;
        
        private Money _200Usd;
        private Money _500Php;
        private Money _150Php;

        [SetUp]
        public void Setup()
        {
            _usd = new Currency() { Id = 1, Name = "USD", Ratio = 1 };
            _php = new Currency() { Id = 2, Name = "PHP", Ratio = 50 };
            _200Usd = new Money() { Currency = _usd, Amount = 200 };
            _500Php = new Money() { Currency = _php, Amount = 500 };
            _150Php = new Money() { Currency = _php, Amount = 150 };
        }

        [Test]
        public void AddMoney_Should_Be_Added_To_Balance()
        {
            Balance balance = new Balance();
            balance.AddMoney(_200Usd);

            var monies = balance.GetAllMonies();

            monies.Should().NotBeNull();
            monies.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void ChargeMoney_Should_Be_Deducted_To_Balance()
        {
            Balance balance = new Balance();
            balance.AddMoney(_500Php);

            balance.ChargeMoney(_150Php);

            var monies = balance.GetAllMonies();
            var newPhpBalance = monies.Find(c => c.Currency == _php);

            newPhpBalance.Amount.Should().Be(350);
        }

        [Test]
        public void ExchangeMoney_Should_Be_Converted_To_ExchangeCurrency()
        {
            Balance balance = new Balance();
            balance.AddMoney(_500Php);

            balance.Exchange(_150Php, _usd);

            var monies = balance.GetAllMonies();
            var newPhpBalance = monies.Find(c => c.Currency == _php);
            var newUsdBalance = monies.Find(c => c.Currency == _usd);

            newPhpBalance.Amount.Should().Be(350);
            newUsdBalance.Amount.Should().Be(3);
        }

        [Test]
        public void ValidateMoney_Should_Block_Transaction_If_No_Enough_Money()
        {
            Balance balance = new Balance();
            balance.AddMoney(_150Php);

            try
            {
                balance.ChargeMoney(_500Php);
            }
            catch (Exception e)
            {
                e.Message.Should().Be("Insufficient amount in balance!");
            }
        }

        [Test]
        public void ValidateMoney_Should_Block_Transaction_If_Money_Not_Exist()
        {
            Balance balance = new Balance();

            try
            {
                balance.ChargeMoney(_200Usd);
            }
            catch (Exception e)
            {
                e.Message.Should().Be("Currency does not exist!");
            }
        }
    }
}
