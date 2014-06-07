using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Province
    {
            public long ID{get;set;}
            public string Name{get;set;}
            public string Code{get;set;}
         
        public Province()
        {       
                    Name ="";   
                    Code ="";   
        }
        
       static Province DynamicCast<T>(object row_data, object row_header) where T : Province
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Province ret = new Province();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Name":
                        ret.Name = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Code":
                        ret.Code = ConvertObject.ToString(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
