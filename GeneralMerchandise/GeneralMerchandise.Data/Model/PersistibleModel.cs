using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal abstract class PersistibleModel<TIdentity> : IIdentifiable<TIdentity> where TIdentity : IComparable<TIdentity>, IComparable
    {
        public abstract TIdentity Identity { get; set; }
    }
}
