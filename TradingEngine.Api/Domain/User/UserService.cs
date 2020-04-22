using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using TradingEngine.Api.Domain.Monies;
using TradingEngine.Api.Implementations;
using TradingEngine.Api.Model.GeneratedContext;
using EntityUser = TradingEngine.Api.Model.GeneratedContext.User;

namespace TradingEngine.Api.Domain.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> UpdateUserBalance(User user)
        {
            foreach (var money in user.Balance.GetAllMonies())
            {
                var userBalance = await _unitOfWork.RepositoryFor<UserBalance>()
                    .Get(b => b.UserId == user.Id && b.CurrencyId == money.Currency.Id);

                if (userBalance == null)
                {
                    var newBalance = new UserBalance()
                    {
                        UserId = user.Id,
                        CurrencyId = money.Currency.Id,
                        Amount = money.Amount
                    };
                    _unitOfWork.RepositoryFor<UserBalance>().Add(newBalance);
                }
                else
                {
                    userBalance.Amount = money.Amount;
                    _unitOfWork.RepositoryFor<UserBalance>().Update(userBalance);
                }

                await _unitOfWork.Commit();
            }

            return await GetUserAsync(user.Username);
        }

        public async Task<User> RegisterUserAsync(string username)
        {
            EntityUser newUser = new EntityUser() { Username = username };

            _unitOfWork.RepositoryFor<EntityUser>().Add(newUser);
            await _unitOfWork.Commit();

            return await GetUserAsync(username);
        }

        public async Task<User> GetUserAsync(string username)
        {
            Balance balance = new Balance();
            
            var userEntity = await _unitOfWork.RepositoryFor<EntityUser>().Get(u => u.Username == username);
            var userBalance = await _unitOfWork.RepositoryFor<UserBalance>().GetMany(u => u.UserId == userEntity.Id);
            var currencies = await _unitOfWork.RepositoryFor<Currency>().GetAll();
            
            /* Populate balance from DB */
            if (userBalance.Any())
            {
                foreach (var item in userBalance)
                {
                    var currency = currencies.SingleOrDefault(c => c.Id == item.CurrencyId);
                    balance.AddMoney(new Money()
                    {
                        Currency = currency,
                        Amount = item.Amount
                    });
                }
            }

            return new User() { Id = userEntity.Id, Username = userEntity.Username, Balance = balance };
        }
    }
}
