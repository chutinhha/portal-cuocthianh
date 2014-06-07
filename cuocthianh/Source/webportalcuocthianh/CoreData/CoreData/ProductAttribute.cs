using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class ProductAttribute
    {
            public long ID{get;set;}
            public long ProductID{get;set;}
            public long AttributeID{get;set;}
            public string Value{get;set;}
            public bool Active{get;set;}
            public long IDExt { get; set; }
            public string AttributeNameExt { get; set; }
            public List<SelectListItem> ListAttributeExt { get; set; }

        public ProductAttribute()
        {       
                    Value ="";
                    AttributeNameExt = "";
        }
        
       static ProductAttribute DynamicCast<T>(object row_data, object row_header) where T : ProductAttribute
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           ProductAttribute ret = new ProductAttribute();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       ret.IDExt = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "ProductID":
                        ret.ProductID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "AttributeID":
                        ret.AttributeID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Value":
                        ret.Value = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                        break;
                   case "AttributeName":
                        ret.AttributeNameExt = ConvertObject.ToString(dt[column]);
                        break;
                     
                
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
