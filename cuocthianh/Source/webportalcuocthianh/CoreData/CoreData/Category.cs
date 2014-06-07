using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;

namespace CoreData
{
    public class Category
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public string Link { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Active { get; set; }
        public long ParentID { get; set; }

        public List<SelectListItem> ListCategoryExt { get; set; }

        public Category()
        {
            Code = "";
            Name = "";
            Image = "";
            Note = "";
            Link = "";
            MetaTitle = "";
            MetaKeyword = "";
            MetaDescription = "";
        }

        static Category DynamicCast<T>(object row_data, object row_header) where T : Category
        {
            // row_data : DataRow
            // row_header : DataColumnCollection
            Category ret = new Category();
            DataRow dt = (DataRow)row_data;

            foreach (DataColumn column in (DataColumnCollection)row_header)
            {
                switch (column.ColumnName)
                {
                    case "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "Code":
                        ret.Code = ConvertObject.ToString(dt[column]);
                        break;
                    case "Name":
                        ret.Name = ConvertObject.ToString(dt[column]);
                        break;
                    case "Image":
                        ret.Image = ConvertObject.ToString(dt[column]);
                        break;
                    case "Note":
                        ret.Note = ConvertObject.ToString(dt[column]);
                        break;
                    case "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
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
                    case "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                        break;
                    case "ParentID":
                        ret.ParentID=ConvertObject.ToLong(dt[column]);
                        break;

                    default:
                        break;
                }
            }
            return ret;
        }

    }
}
