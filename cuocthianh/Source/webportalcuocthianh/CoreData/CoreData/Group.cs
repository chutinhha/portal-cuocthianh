using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Group
    {
            public long ID{get;set;}
            public string Name{get;set;}
            public string Code{get;set;}
            public string Note{get;set;}
            public bool Active{get;set;}
         
        public Group()
        {       
                    Name ="";   
                    Code ="";   
                    Note ="";   
        }
        
       static Group DynamicCast<T>(object row_data, object row_header) where T : Group
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Group ret = new Group();
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