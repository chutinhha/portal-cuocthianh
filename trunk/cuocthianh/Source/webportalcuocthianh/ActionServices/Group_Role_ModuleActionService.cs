using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IGroup_Role_ModuleActionService
    {
        long Save(object _model);
        CoreData.Group_Role_Module GetByID(long _id);
        IList<CoreData.Group_Role_Module> GetList();
        IList<CoreData.Group_Role_Module> GetListByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where);
        CoreData.Group_Role_Module GetOneByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where);
        IList<CoreData.Group_Role_Module> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class Group_Role_ModuleActionService:IGroup_Role_ModuleActionService
    {
       Group_Role_ModuleService Service;

       public Group_Role_ModuleActionService(Group_Role_ModuleService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Group_Role_Module GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Group_Role_Module> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Group_Role_Module> GetListByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Group_Role_Module GetOneByLINQ(Func<CoreData.Group_Role_Module, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Group_Role_Module> GetList(string _searchstring)
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
