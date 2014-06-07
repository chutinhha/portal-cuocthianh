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

    public interface IGroupActionService
    {
        long Save(object _model);
        CoreData.Group GetByID(long _id);
        IList<CoreData.Group> GetList();
        IList<CoreData.Group> GetListByLINQ(Func<CoreData.Group, Boolean> _where);
        CoreData.Group GetOneByLINQ(Func<CoreData.Group, Boolean> _where);
        IList<CoreData.Group> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetSelectList(int id, string type="");
    }

    public partial class GroupActionService:IGroupActionService
    {
       GroupService Service;

       public GroupActionService(GroupService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Group GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Group> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Group> GetListByLINQ(Func<CoreData.Group, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Group GetOneByLINQ(Func<CoreData.Group, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Group> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method


       public virtual List<SelectListItem> GetSelectList(int id, string type = "")
       {
           return Service.GetSelectList(id, type);
       }

        #endregion

    }
         
}
