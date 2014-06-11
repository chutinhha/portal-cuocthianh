using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class CategoryArticle
    {
            public long ID{get;set;}
            public string Code{get;set;}
            public string Name{get;set;}
            public string Description{get;set;}
            public string Link{get;set;}
            public string Image{get;set;}
            public string MetaTitle{get;set;}
            public string MetaKeyword{get;set;}
            public string MetaDescription{get;set;}
            public bool Active{get;set;}
         
        public CategoryArticle()
        {       
                    Code ="";   
                    Name ="";   
                    Description ="";   
                    Link ="";   
                    Image ="";   
                    MetaTitle ="";   
                    MetaKeyword ="";   
                    MetaDescription ="";   
        }
        
       static CategoryArticle DynamicCast<T>(object row_data, object row_header) where T : CategoryArticle
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           CategoryArticle ret = new CategoryArticle();
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
                      case  "Description":
                        ret.Description = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Image":
                        ret.Image = ConvertObject.ToString(dt[column]);
                       break;
                      case  "MetaTitle":
                        ret.MetaTitle = ConvertObject.ToString(dt[column]);
                       break;
                      case  "MetaKeyword":
                        ret.MetaKeyword = ConvertObject.ToString(dt[column]);
                       break;
                      case  "MetaDescription":
                        ret.MetaDescription = ConvertObject.ToString(dt[column]);
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
