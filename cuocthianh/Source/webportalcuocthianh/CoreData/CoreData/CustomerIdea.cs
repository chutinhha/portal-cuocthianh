using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class CustomerIdea
    {
            public long ID{get;set;}
            public long UserID{get;set;}
            public string Content{get;set;}
            public string Attachment { get; set; }
            public int Type{get;set;}
            public long OrderID{get;set;}
            public string Email { get; set; }
            public string UserNameExt { get; set; }
            public DateTime DateCreate { get; set; }

            public CustomerIdea()
            {
                Content = "";
                Attachment = "";
                Email = "";
                UserNameExt = "";
            }
        
       static CustomerIdea DynamicCast<T>(object row_data, object row_header) where T : CustomerIdea
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           CustomerIdea ret = new CustomerIdea();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                   case "ID":
                       ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "UserID":
                       ret.UserID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "Content":
                       ret.Content = ConvertObject.ToString(dt[column]);
                       break;
                   case "Type":
                       ret.Type = ConvertObject.ToInt(dt[column]);
                       break;
                   case "OrderID":
                       ret.OrderID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "Attachment":
                       ret.Attachment = ConvertObject.ToString(dt[column]);
                       break;
                   case "Email":
                       ret.Email = ConvertObject.ToString(dt[column]);
                       break;
                   case "UserName":
                       ret.UserNameExt = ConvertObject.ToString(dt[column]);
                       break;
                   case "DateCreate":
                       ret.DateCreate = ConvertObject.ToDateTime(dt[column]);
                       break;
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
