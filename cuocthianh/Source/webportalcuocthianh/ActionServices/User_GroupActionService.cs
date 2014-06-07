using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IUser_GroupActionService
    {
        long Save(object _model);
        CoreData.User_Group GetByID(long _id);
        IList<CoreData.User_Group> GetList();
        IList<CoreData.User_Group> GetListByLINQ(Func<CoreData.User_Group, Boolean> _where);
        CoreData.User_Group GetOneByLINQ(Func<CoreData.User_Group, Boolean> _where);
        IList<CoreData.User_Group> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class User_GroupActionService:IUser_GroupActionService
    {
       User_GroupService Service;

       public User_GroupActionService(User_GroupService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.User_Group GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.User_Group> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.User_Group> GetListByLINQ(Func<CoreData.User_Group, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.User_Group GetOneByLINQ(Func<CoreData.User_Group, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.User_Group> GetList(string _searchstring)
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
