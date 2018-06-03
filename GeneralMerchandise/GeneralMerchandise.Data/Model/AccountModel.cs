using GeneralMerchandise.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal sealed class AccountModel : PersistibleModel<int>
    {
        private static readonly int DEAFULT_IDENTITY = 0;

        
        public static AccountModel New(string username, string password, AccessType accountType)
        {
            return new AccountModel
            {
                Identity = DEAFULT_IDENTITY,
                Username = username,
                Password = password,
                AccountType = accountType,
                IsActive = true
            };
        }

        private AccountModel() { }

        public override int Identity { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public AccessType AccountType { get; set; }
        public bool IsActive { get; set; }
    }
}
