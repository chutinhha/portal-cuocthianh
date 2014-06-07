using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class User_Group
    {
            public long ID{get;set;}
            public long UserID{get;set;}
            public int GroupID{get;set;}
         
        public User_Group()
        {       
        }
        
       static User_Group DynamicCast<T>(object row_data, object row_header) where T : User_Group
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           User_Group ret = new User_Group();
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
                      case  "GroupID":
                        ret.GroupID = ConvertObject.ToInt(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
