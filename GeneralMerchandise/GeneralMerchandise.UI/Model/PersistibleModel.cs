using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Model
{
    internal abstract class PersistibleModel : BaseModel
    {

        protected abstract void SaveMethod();
        protected abstract void EditMethod();
        protected abstract void DeleteMethod();

        public void Save()
        {

        }

        public void BeginEdit()
        {

        }

        public void ApplyEdit()
        {

        }

        public void CancelEdit()
        {

        }

        public void Delete()
        {

        }


    }
}
