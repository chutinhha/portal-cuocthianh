using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Comment
    {
            public long ID{get;set;}
            public string Name{get;set;}
            public string Email{get;set;}
            public string Phone{get;set;}
            public string Address{get;set;}
            public string Comment{get;set;}
            public long ReferenceID{get;set;}
            public string CommentType{get;set;}
            public long ParentID{get;set;}
            public DateTime PostDate{get;set;}
            public bool Active{get;set;}
         
        public Comment()
        {       
                    Name ="";   
                    Email ="";   
                    Phone ="";   
                    Address ="";   
                    Comment ="";   
                    CommentType ="";   
        }
        
       static Comment DynamicCast<T>(object row_data, object row_header) where T : Comment
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Comment ret = new Comment();
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
                      case  "Email":
                        ret.Email = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Phone":
                        ret.Phone = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Address":
                        ret.Address = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Comment":
                        ret.Comment = ConvertObject.ToString(dt[column]);
                       break;
                      case  "ReferenceID":
                        ret.ReferenceID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "CommentType":
                        ret.CommentType = ConvertObject.ToString(dt[column]);
                       break;
                      case  "ParentID":
                        ret.ParentID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "PostDate":
                        ret.PostDate = ConvertObject.ToDateTime(dt[column]);
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
