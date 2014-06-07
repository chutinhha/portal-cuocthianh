using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class Order
    {
            public long ID{get;set;}
            public long UserID{get;set;}
            public int TotalPrice{get;set;}
            public int Ship_Fee{get;set;}
            public string Name{get;set;}
            public string Address{get;set;}
            public string Phone{get;set;}
            public DateTime CreateDate{get;set;}
            public DateTime DateCompleted{get;set;}
            public int PaymentMethod{get;set;}
            public int ShipMethod{get;set;}
            public int Status{get;set;}
            public string Note{get;set;}
            public bool Active{get;set;}
            public IList<SelectListItem> ListStatusExt { get; set; }


            public string UserNameExt { get; set; }
            public List<SelectListItem> ListUserNameExt { get; set; }
         

        public Order()
        {       
                    Name ="";   
                    Address ="";   
                    Phone ="";   
                    Note ="";
                    UserNameExt = "";
        }
        
       static Order DynamicCast<T>(object row_data, object row_header) where T : Order
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Order ret = new Order();
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
                      case  "TotalPrice":
                        ret.TotalPrice = ConvertObject.ToInt(dt[column]);
                       break;
                      case  "Ship_Fee":
                        ret.Ship_Fee = ConvertObject.ToInt(dt[column]);
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
                      case  "CreateDate":
                        ret.CreateDate = ConvertObject.ToDateTime(dt[column]);
                       break;
                      case  "DateCompleted":
                        ret.DateCompleted = ConvertObject.ToDateTime(dt[column]);
                       break;
                      case  "PaymentMethod":
                        ret.PaymentMethod = ConvertObject.ToInt(dt[column]);
                       break;
                      case  "ShipMethod":
                        ret.ShipMethod = ConvertObject.ToInt(dt[column]);
                       break;
                      case  "Status":
                        ret.Status = ConvertObject.ToInt(dt[column]);
                       break;
                      case  "Note":
                        ret.Note = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                       break;
                
                   default:
                       break;
               }
               
           }

           return ret;
       }
        
    }
}
