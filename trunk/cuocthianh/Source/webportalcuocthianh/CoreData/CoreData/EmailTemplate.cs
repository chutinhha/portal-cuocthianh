using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class EmailTemplate
    {
            public long ID{get;set;}
            public string Code{get;set;}
            public string Name{get;set;}
            public string Template{get;set;}
            public int Type{get;set;}
         
        public EmailTemplate()
        {       
                    Code ="";   
                    Name ="";   
                    Template ="";   
        }
        
       static EmailTemplate DynamicCast<T>(object row_data, object row_header) where T : EmailTemplate
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           EmailTemplate ret = new EmailTemplate();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Code":
                        ret.Code = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Name":
                        ret.Name = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Template":
                        ret.Template = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Type":
                        ret.Type = ConvertObject.ToInt(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
