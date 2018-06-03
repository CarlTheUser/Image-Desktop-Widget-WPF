
using System;

namespace DataLayer.Model
{
    internal abstract class BaseModel<TKey> where TKey: IComparable<TKey>, IComparable
    {

        //an Id/Primary key must be IComparable (int, string, etc your struct) to support many key types.
        //case scenario -> complex Primary keys in database like 2014-10261 (year column and auto_increment column) or 231asdBge538396e (multi column/etc) (composite)
        public abstract TKey Id { get; set; }
    }
   
}
