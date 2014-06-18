using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;

namespace CoreData
{
    public partial class Article
    {
       public long ID { get; set; }
       public string Title { get; set; }
       public long CateID { get; set; }
       public string ShortDescription { get; set; }
       public long UserID { get; set; }
       public string Content { get; set; }
       public DateTime UpdateDate { get; set; }
       public int View { get; set; }
       public string Link { get; set; }
       public string Image { get; set; }
       public string MetaTitle { get; set; }
       public string MetaKeyword { get; set; }
       public string MetaDescription { get; set; }
       public bool Active { get; set; }
       public bool ShowHomePage { get; set; }

       public string CategoryNameExt { get; set; }
       public string UserNameExt { get; set; }

        /// <summary>
        /// Pager Variable
        /// </summary>
       public int pageExt { get; set; }
       public int PageSizeExt { get; set; }
       public int TotalPageExt { get; set; }

       public List<SelectListItem> ListCategoryExt { get; set; }
       public List<Article> ListSameArticleExt { get; set; }

       public Article()
       {
           Title = "";
           ShortDescription = "";
           Content = "";
           Link = "";
           CategoryNameExt = "";
           UserNameExt = "";
           Image = "";
           MetaTitle = "";
           MetaKeyword = "";
           MetaDescription = "";
       }


       static Article DynamicCast<T>(object row_data, object row_header) where T : Article
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           Article ret = new Article();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {

                   case "ID":
                       ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "Title":
                       ret.Title = ConvertObject.ToString(dt[column]);
                       break;
                   case "CateID":
                       ret.CateID = ConvertObject.ToInt(dt[column]);
                       break;
                   case "ShortDescription":
                       ret.ShortDescription = ConvertObject.ToString(dt[column]);
                       break;
                   case "UserID":
                       ret.UserID = ConvertObject.ToInt(dt[column]);
                       break;
                   case "UserName":
                       ret.UserNameExt = ConvertObject.ToString(dt[column]);
                       break;
                   case "Content":
                       ret.Content = ConvertObject.ToString(dt[column]);
                       break;
                   case "UpdateDate":
                       ret.UpdateDate = ConvertObject.ToDateTime(dt[column]);
                       break;
                   case "MetaTitle":
                       ret.MetaTitle = ConvertObject.ToString(dt[column]);
                       break;
                   case "MetaKeyword":
                       ret.MetaKeyword = ConvertObject.ToString(dt[column]);
                       break;
                   case "MetaDescription":
                       ret.MetaDescription = ConvertObject.ToString(dt[column]);
                       break;
                   case "View":
                       ret.View = ConvertObject.ToInt(dt[column]);
                       break;
                   case "Link":
                       ret.Link = ConvertObject.ToString(dt[column]);
                       break;
                   case "Image":
                       ret.Image = ConvertObject.ToString(dt[column]);
                       break;
                   case "Active":
                       ret.Active = ConvertObject.ToBool(dt[column]);
                       break;
                   case "ShowHomePage":
                       ret.ShowHomePage = ConvertObject.ToBool(dt[column]);
                       break;
                   default:
                       break;
               }
           }
           return ret;
       }
    }
}
