using GeneralMerchandise.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Model
{
    internal class AccountModel : PersistibleModel
    {

        private int id;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private AccessType accessType;

        public AccessType AccessType
        {
            get => accessType;
            set
            {
                accessType = value;
                OnPropertyChanged("AccessType");
            }
        }

        private bool active;

        public bool IsActive
        {
            get => active;
            set
            {
                active = value;
                OnPropertyChanged("IsActive");
            }
        }

        protected override void DeleteMethod()
        {
            throw new NotImplementedException();
        }

        protected override void EditMethod()
        {
            throw new NotImplementedException();
        }

        protected override void SaveMethod()
        {
            throw new NotImplementedException();
        }
    }
}
