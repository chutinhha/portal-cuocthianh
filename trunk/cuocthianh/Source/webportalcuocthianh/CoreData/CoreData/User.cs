using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class User
    {
            public long ID{get;set;}
            public string UserName{get;set;}
            public string Password{get;set;}
            public string Name{get;set;}
            public string Address{get;set;}
            public string Phone{get;set;}
            public string Email{get;set;}
            public bool Active{get;set;}
            public string ProvinceNameExt { get; set; }
            public int GroupIDExt { get; set; }
            public int ProvinceIDExt { get; set; }
            public int DictrictIDExt { get; set; }
            public string TempPassWordExt { get; set; }
            public List<SelectListItem> ListGroupExt;
            public List<SelectListItem> ListProvinceExt;
            public List<SelectListItem> ListDictrictExt;
            public List<SelectListItem> ListUserManegerExt;
            public Examinee ExamineeExt { get; set; }
        public User()
        {       
                    UserName ="";   
                    Password ="";   
                    Name ="";   
                    Address ="";   
                    Phone ="";   
                    Email ="";
                    ProvinceNameExt = "";
                    TempPassWordExt = "";
        }
        
       static User DynamicCast<T>(object row_data, object row_header) where T : User
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           User ret = new User();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                   case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                   case  "UserName":
                        ret.UserName = ConvertObject.ToString(dt[column]);
                       break;
                   case  "Password":
                        ret.Password = ConvertObject.ToString(dt[column]);
                       break;
                   case  "Name":
                        ret.Name = ConvertObject.ToString(dt[column]);
                       break;
                   case  "Address":
                        ret.Address = ConvertObject.ToString(dt[column]);
                       break;
                   case  "Phone":
                        ret.Phone = ConvertObject.ToString(dt[column]);
                       break;
                   case  "Email":
                        ret.Email = ConvertObject.ToString(dt[column]);
                       break;
                   case "GroupID":
                       ret.GroupIDExt = ConvertObject.ToInt(dt[column]);
                       break;
                   case  "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                       break;
                    case "ProvinceIDExt":
                       ret.ProvinceIDExt = ConvertObject.ToInt(dt[column]);
                       break;
                    case "ProvinceNameExt":
                       ret.ProvinceNameExt = ConvertObject.ToString(dt[column]);
                       break;

                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
