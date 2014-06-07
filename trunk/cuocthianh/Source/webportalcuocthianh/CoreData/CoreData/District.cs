using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class District
    {
            public long ID{get;set;}
            public string Code{get;set;}
            public string Name{get;set;}
            public long ProvinceID{get;set;}
            public string ProvinceNameExt { get; set; }
            public List<SelectListItem> ListProvinceExt { get; set; }
         
        public District()
        {       
                    Code ="";   
                    Name ="";
                    ProvinceNameExt = "";
        }
        
       static District DynamicCast<T>(object row_data, object row_header) where T : District
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           District ret = new District();
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
                      case  "ProvinceID":
                        ret.ProvinceID = ConvertObject.ToLong(dt[column]);
                       break;
                      case "ProvinceName":
                       ret.ProvinceNameExt = ConvertObject.ToString(dt[column]);
                       break;
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
