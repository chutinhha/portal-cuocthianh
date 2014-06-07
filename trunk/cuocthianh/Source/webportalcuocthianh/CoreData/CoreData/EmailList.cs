using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class EmailList
    {
            public long ID{get;set;}
            public string Email{get;set;}
            public string TitleExt { get; set; }
            public string BodyExt { get; set; }
         
        public EmailList()
        {       
                    Email ="";   
        }
        
       static EmailList DynamicCast<T>(object row_data, object row_header) where T : EmailList
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           EmailList ret = new EmailList();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Email":
                        ret.Email = ConvertObject.ToString(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
