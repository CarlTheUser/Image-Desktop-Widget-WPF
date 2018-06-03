using DataLayer.Model;
using System.Collections.Generic;

namespace DataLayer.ModelService
{
    internal abstract class FramedImageModelPersistence : IModelPersistence<FramedImageModel, int>
    {
        //serves as a base class for FramedImageModelPersistence which
        //will be extended by specific model data providers (XML / SQL / WebService / etc)
        //that is if youre not into using ORMs like linq2sql entity framework, etc which seems to be tied to using ms
        

        //further add model specific methods (if there's any)
        public abstract int GetCount();

        //inherited methods
        public abstract int Save(FramedImageModel model);

        public abstract void Edit(int id, FramedImageModel updatedModel);

        public abstract void Delete(int id);

        public abstract IEnumerable<FramedImageModel> GetAll();

        public abstract FramedImageModel GetById(int id);
        
    }
}
