using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class User_Role_Module
    {
            public long ID{get;set;}
            public long UserID{get;set;}
            public long ModuleID{get;set;}
            public string Role{get;set;}
            public string UserNameExt { get; set; }
            public string GroupNameExt { get; set; }
            public string ModuleNameExt { get; set; }
            public bool CExt { get; set; }
            public bool RExt { get; set; }
            public bool UExt { get; set; }
            public bool DExt { get; set; }
            public List<SelectListItem> listmoduleExt;
            public List<SelectListItem> listUserExt;
         
        public User_Role_Module()
        {       
                    Role ="";
                    UserNameExt = "";
                    GroupNameExt = "";
                    ModuleNameExt = "";
        }
        
       static User_Role_Module DynamicCast<T>(object row_data, object row_header) where T : User_Role_Module
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           User_Role_Module ret = new User_Role_Module();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "UserID":
                        ret.UserID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "ModuleID":
                        ret.ModuleID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Role":
                        ret.Role = ConvertObject.ToString(dt[column]);
                       break;
                      case "UserName":
                       ret.UserNameExt = ConvertObject.ToString(dt[column]);
                       break;
                   case"GroupName":
                       ret.GroupNameExt = ConvertObject.ToString(dt[column]);
                       break;
                   case "ModuleName":
                       ret.ModuleNameExt = ConvertObject.ToString(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
