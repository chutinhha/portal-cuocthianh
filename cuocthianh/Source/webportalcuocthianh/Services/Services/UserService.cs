using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web.Mvc;
namespace Services
{

    public partial class UserService
    {
       readonly IUserEntity entity;
       readonly IUser_GroupEntity Group;
       readonly IProvinceEntity menuprovince;
       readonly IUser_Role_ModuleEntity userrole;
       readonly IExamineeEntity examinee;

       public UserService(IUserEntity entity, IUser_GroupEntity group, IProvinceEntity province, IUser_Role_ModuleEntity user,
           IExamineeEntity examinee)
       {
           this.entity = entity;
           this.Group = group;
           this.menuprovince = province;
           this.userrole = user ;
           this.examinee = examinee;
       }

       #region Main Method

       /// <summary>
       /// Save 
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public long Save(object _model)
       {
           try
           {
               var obj = (User)_model;
               var id = obj.ID;
               if (id == 0)
               {
                   obj.Password = Security.EncryptString(obj.Password);
                   obj.Active = true;
                   id = entity.Save(obj, Table.Users.ToString());

                   //luu thong tin nguoi du thi
                   obj.ExamineeExt.Code = StringHelper.GenerateCode((int)id);
                   obj.ExamineeExt.UserID = id;
                   examinee.Save(obj.ExamineeExt,Table.Examinee.ToString());
               }
               else
               {

                   if (obj.Password == null || obj.Password == "")
                   {
                       obj.Password = GetByID(obj.ID).Password;
                   }
                   else
                   {
                       obj.Password = Security.EncryptString(obj.Password);
                   }
                   if (obj.UserName == null || obj.UserName == "")
                   {
                       obj.UserName = GetByID(obj.ID).UserName;
                   }
                   id = entity.Update(obj, Table.Users.ToString());
                 
               }

               return id;


           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.User GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Users.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User> GetList()
       {
           try
           {
               return entity.GetAll(Table.Users.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User> GetListByLINQ(Func<CoreData.User, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Users.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.User GetOneByLINQ(Func<CoreData.User, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Users.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.User> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Users.ToString()).ToList();
           }
           catch { return null; }
       }


       /// <summary>
       /// Delete
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public bool Delete(object _model)
       {
           try
           {
               var data =(CoreData.User)_model;
               entity.Delete((CoreData.User)_model, Table.Users.ToString());
               var group = Group.Get(c=>c.UserID.Equals(data.ID), Table.Users.ToString());
               Group.Delete(group, Table.User_Group.ToString());
               var role = userrole.GetMany(c => c.UserID.Equals(data.ID), Table.User_Role_Module.ToString());
               foreach (var i in role)
               {
                   userrole.Delete(i, Table.User_Role_Module.ToString());
               }
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method
        /// <summary>
        /// Get User By Group
        /// </summary>
        /// <param name="_groupid"></param>
        /// <returns></returns>
       public IList<CoreData.User> GetByGroup(int _groupid)
       {
           try
           {
               return entity.GetByCusTomSQL(string.Format(SQLCommand.GetUserByGroup, _groupid)).ToList();
           }
           catch { return null; }

       }
       /// <summary>
       /// Get User by User Name
       /// </summary>
       /// <param name="username"></param>
       /// <returns></returns>
       public CoreData.User GetByUserName(string username)
       {
           try
           {
               var userdata = entity.GetByCusTomSQL(string.Format(SQLCommand.GetUserByUserName, username)).ToList().First();

                userdata.ExamineeExt = examinee.Get(c=>c.UserID.Equals(userdata.ID),Table.Examinee.ToString());
                userdata.ExamineeCodeExt = userdata.ExamineeExt.Code;
                userdata.ImageExt = userdata.ExamineeExt.Image;
                userdata.ExamineeIDExt = userdata.ExamineeExt.ID;
                if (userdata.ExamineeExt == null)
                    userdata.ExamineeExt = new Examinee();
                return userdata;
           }
           catch { return null; }
       }
       
       public CoreData.User GetByEmail(string email)
       {
           try
           {
               return entity.GetByCusTomSQL(string.Format(SQLCommand.GetUserByEmail, email)).ToList().First();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.User GetByCustomID(long _id)
       {
           try
           {
               return entity.GetByCusTomSQL(string.Format(SQLCommand.GetUserByID,_id)).ToList().First();
           }
           catch { return null; }
       }

       public bool Login(User _user,ref string username, ref int userid, ref int group)
       {
           try
           {
               var pass = Security.EncryptString(_user.Password);
               var data =entity.Get(x => x.UserName==_user.UserName && x.Password== pass, Table.Users.ToString());
               if (data != null)
               {
                   userid = int.Parse(data.ID.ToString());
                   group = Group.Get(c => c.UserID == data.ID, Table.User_Group.ToString()).GroupID;
                   username = data.UserName;
                   return true; 
               }
               else{
                  return false;
               }
                   
           }
           catch { return false; }
       }

       public List<SelectListItem> GetListProvince(int id)
       {
           var lst = new List<SelectListItem>();
           var listprovince = menuprovince.GetAll(Table.Province.ToString());
           foreach (var c in listprovince)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           return lst;
       }

       public List<SelectListItem> GetListUserManger(int id)
       {
           var lst = new List<SelectListItem>();
           var listuser = GetByGroup(2);
           foreach (var c in listuser)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           var all = new SelectListItem();
           all.Text = "Tất cả";
           all.Value = "0";
           lst.Insert(0, all);
           return lst;
       }

       public bool LogOut()
       {
           return true;
       }

        #endregion 
    
    }
         
}
