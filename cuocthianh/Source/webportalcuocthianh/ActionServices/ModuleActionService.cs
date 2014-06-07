using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IModuleActionService
    {
        long Save(object _model);
        CoreData.Module GetByID(long _id);
        IList<CoreData.Module> GetList();
        IList<CoreData.Module> GetListByLINQ(Func<CoreData.Module, Boolean> _where);
        CoreData.Module GetOneByLINQ(Func<CoreData.Module, Boolean> _where);
        IList<CoreData.Module> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class ModuleActionService:IModuleActionService
    {
       ModuleService Service;

       public ModuleActionService(ModuleService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Module GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Module> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Module> GetListByLINQ(Func<CoreData.Module, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Module GetOneByLINQ(Func<CoreData.Module, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Module> GetList(string _searchstring)
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
