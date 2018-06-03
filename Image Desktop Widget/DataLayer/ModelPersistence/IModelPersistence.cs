using System;
using System.Collections.Generic;
using DataLayer.Model;
namespace DataLayer.ModelService
{
    internal interface IModelPersistence<TModel, TKey> where TModel: BaseModel<TKey> where TKey: IComparable, IComparable<TKey>
    {

        //generic persistence methods (your typical CRUD)
        //base class for model data providers

        TKey Save(TModel model);

        void Edit(TKey id, TModel updatedModel);

        void Delete(TKey id);

        TModel GetById(TKey id);

        IEnumerable<TModel> GetAll(); 

    }
}
