using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
using System.Web.Mvc;

namespace ActionServices
{

    public interface ICategoryActionService
    {
        long Save(object _model);
        CoreData.Category GetByID(long _id);
        IList<CoreData.Category> GetList();
        IList<CoreData.Category> GetListByLINQ(Func<CoreData.Category, Boolean> _where);
        CoreData.Category GetOneByLINQ(Func<CoreData.Category, Boolean> _where);
        IList<CoreData.Category> GetList(string _searchstring);
        bool Delete(object _model);
        bool ValidateExist(Category _entity, List<Category> _objects, string _propertiesname, out string error);
    }

    public partial class CategoryActionService:ICategoryActionService
    {
       CategoryService Service;

       public CategoryActionService(CategoryService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Category GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Category> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Category> GetListByLINQ(Func<CoreData.Category, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Category GetOneByLINQ(Func<CoreData.Category, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Category> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
       public bool ValidateExist(Category _entity, List<Category> _objects, string _propertiesname, out string error)
       {
           return Service.ValidateExist(_entity, _objects, _propertiesname, out error);
       }
        #endregion

    }
         
}
