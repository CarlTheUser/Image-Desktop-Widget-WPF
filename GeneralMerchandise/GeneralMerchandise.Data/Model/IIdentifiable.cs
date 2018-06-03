using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal interface IIdentifiable <T> where T : IComparable<T>, IComparable
    {
        T Identity { get; }
    }
}
