using System;
using System.Collections.Generic;

namespace TradingEngine.Api.Model.GeneratedContext
{
    public partial class User
    {
        public User()
        {
            UserBalance = new HashSet<UserBalance>();
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserBalance> UserBalance { get; set; }
    }
}
