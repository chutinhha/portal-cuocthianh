using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class PictureExam
    {
            public long ID{get;set;}
            public long ExamineeID{get;set;}
            public string Title{get;set;}
            public string Image{get;set;}
            public string Link{get;set;}
            public DateTime PostDate { get; set; }
            public string ThumnailImg { get; set; }
            public bool Active{get;set;}
         
        public PictureExam()
        {       
                    Title ="";   
                    Image ="";   
                    Link ="";
                    ThumnailImg = "";
        }
        
       static PictureExam DynamicCast<T>(object row_data, object row_header) where T : PictureExam
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           PictureExam ret = new PictureExam();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                      case  "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "ExamineeID":
                        ret.ExamineeID = ConvertObject.ToLong(dt[column]);
                       break;
                      case  "Title":
                        ret.Title = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Image":
                        ret.Image = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
                       break;
                      case  "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                       break;
                      case "PostDate":
                       ret.PostDate = ConvertObject.ToDateTime(dt[column]);
                       break;
                      case "ThumnailImg":
                       ret.ThumnailImg = ConvertObject.ToString(dt[column]);
                       break;
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
