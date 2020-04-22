using Moq;
using NUnit.Framework;
using TradingEngine.Api.Implementations;

namespace TradingEngine.Api.Tests.Domain.User
{
    [TestFixture]
    class UserServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }
    }
}
