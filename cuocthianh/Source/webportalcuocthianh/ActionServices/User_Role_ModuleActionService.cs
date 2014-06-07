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

    public interface IUser_Role_ModuleActionService
    {
        long Save(object _model);
        CoreData.User_Role_Module GetByID(long _id);
        IList<CoreData.User_Role_Module> GetList();
        IList<CoreData.User_Role_Module> GetListByLINQ(Func<CoreData.User_Role_Module, Boolean> _where);
        CoreData.User_Role_Module GetOneByLINQ(Func<CoreData.User_Role_Module, Boolean> _where);
        IList<CoreData.User_Role_Module> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListModule(int id);
    }

    public partial class User_Role_ModuleActionService:IUser_Role_ModuleActionService
    {
       User_Role_ModuleService Service;

       public User_Role_ModuleActionService(User_Role_ModuleService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.User_Role_Module GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.User_Role_Module> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.User_Role_Module> GetListByLINQ(Func<CoreData.User_Role_Module, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.User_Role_Module GetOneByLINQ(Func<CoreData.User_Role_Module, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.User_Role_Module> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetListModule(int id)
       {
           return Service.GetListModule(id);
       }

        #endregion

    }
         
}
