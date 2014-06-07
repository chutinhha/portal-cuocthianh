using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Group_Role_Module
    {
            public long ID{get;set;}
            public long GroupID{get;set;}
            public long MouleID{get;set;}
            public string Role{get;set;}
         
        public Group_Role_Module()
        {       
                    Role ="";   
        }
        
       static Group_Role_Module DynamicCast<T>(object row_data, object row_header) where T : Group_Role_Module
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Group_Role_Module ret = new Group_Role_Module();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "GroupID":
                        ret.GroupID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "MouleID":
                        ret.MouleID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Role":
                        ret.Role = ConvertObject.ToString(dt[column]);
                       break;
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
