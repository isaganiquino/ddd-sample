using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingEngine.Api.Domain.Monies;
using TradingEngine.Api.Model.GeneratedContext;

namespace TradingEngine.Api.Domain.User
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string username);
        Task<User> RegisterUserAsync(string username);
        public Task<User> UpdateUserBalance(User user);
    }
}
