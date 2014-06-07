using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IModelActionService
    {
        long Save(object _model);
        CoreData.Model GetByID(long _id);
        IList<CoreData.Model> GetList();
        IList<CoreData.Model> GetListByLINQ(Func<CoreData.Model, Boolean> _where);
        CoreData.Model GetOneByLINQ(Func<CoreData.Model, Boolean> _where);
        IList<CoreData.Model> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class ModelActionService:IModelActionService
    {
       ModelService Service;

       public ModelActionService(ModelService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {

           return Service.Save(_model);
       }

       public virtual CoreData.Model GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Model> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Model> GetListByLINQ(Func<CoreData.Model, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Model GetOneByLINQ(Func<CoreData.Model, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Model> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
        #endregion

    }
         
}
