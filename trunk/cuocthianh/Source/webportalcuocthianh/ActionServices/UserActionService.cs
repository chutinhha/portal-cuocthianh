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

    public interface IUserActionService
    {
        long Save(object _model);
        CoreData.User GetByID(long _id);
        IList<CoreData.User> GetList();
        IList<CoreData.User> GetListByLINQ(Func<CoreData.User, Boolean> _where);
        CoreData.User GetOneByLINQ(Func<CoreData.User, Boolean> _where);
        IList<CoreData.User> GetList(string _searchstring);
        bool Delete(object _model);
        IList<CoreData.User> GetByGroup(int _groupid);
        CoreData.User GetByCustomID(long _id);
        bool Login(CoreData.User _model,ref string username, ref int userid, ref int group);
        List<SelectListItem> GetListProvince(int id);
        CoreData.User GetByUserName(string username);
        List<SelectListItem> GetListUserManger(int id);
        CoreData.User GetByEmail(string email);
       
    }

    public partial class UserActionService:IUserActionService
    {
       UserService Service;
       User_GroupService GroupService;
        User_Role_ModuleService RoleService;
        ModuleService ModuleService;
        EmailListService EmailService;
       public UserActionService(UserService _Service,User_GroupService _Group,User_Role_ModuleService role,
           ModuleService module, EmailListService Email)
       {
           Service = _Service;
           GroupService = _Group;
           RoleService = role;
           ModuleService = module;
           EmailService = Email;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           var id = Service.Save(_model);
           if (id > 0)
           {
               User_Group g = new User_Group();
               g.GroupID = ((User)_model).GroupIDExt;
               g.UserID = id;
               GroupService.Save(g);
               EmailService.Save(((User)_model).Email);
               var module = ModuleService.GetList();
               var check = RoleService.GetListByLINQ(c => c.UserID.Equals(id));
               if (check == null||check.Count==0)
               {
                   foreach (var item in module)
                   {
                       User_Role_Module m = new User_Role_Module();
                       m.UserID = id;
                       m.ModuleID = item.ID;
                       m.Role = "";
                       RoleService.Save(m);
                   }
               }
           }
           return id;
       }

       public virtual CoreData.User GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public CoreData.User GetByUserName(string username)
       {
           return Service.GetByUserName(username);
       }

       public CoreData.User GetByEmail(string email)
       {
           return Service.GetByEmail(email);
       }

       public virtual IList<CoreData.User> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.User> GetListByLINQ(Func<CoreData.User, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.User GetOneByLINQ(Func<CoreData.User, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.User> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual IList<CoreData.User> GetByGroup(int _groupid)
       {
           return Service.GetByGroup(_groupid);
       }
       public virtual CoreData.User GetByCustomID(long _id)
       {
           return Service.GetByCustomID(_id);
       }
       public virtual bool Login(User _model,ref string username, ref int userid, ref int group)
       {
           return Service.Login(_model,ref username,ref  userid, ref  group);
       }

       public virtual List<SelectListItem> GetListProvince(int id)
       {
           return Service.GetListProvince(id);

       }

       public virtual List<SelectListItem> GetListUserManger(int id)
       {
           return Service.GetListUserManger(id);
       }

        #endregion

    }
         
}
