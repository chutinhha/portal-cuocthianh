using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;
using System.Data;
using System.Web.Mvc;

namespace CoreData
{
    public partial class Banner_Logo
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ValueDefine.BannerLogoType Type { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }

        public long ProductCateID { get; set; }
        public DateTime ShowFrom { get; set; }
        public DateTime ShowTo { get; set; }
        public string Link { get; set; }
        public ValueDefine.BannerLogoPosition Position { get; set; }

        public List<SelectListItem> ListCategoryExt { get; set; }
        public List<SelectListItem> ListTypeExt { get; set; }
        public List<SelectListItem> ListPositionExt { get; set; }

        public Banner_Logo()
        {
            Code = "";
            Name = "";
            Image = "";
            Type = ValueDefine.BannerLogoType.Banner;
            Note = "";
            Link = "";
            Position = ValueDefine.BannerLogoPosition.ShideShow;
        }

        static Banner_Logo DynamicCast<T>(object row_data, object row_header) where T : Banner_Logo
        {
            // row_data : DataRow
            // row_header : DataColumnCollection
            Banner_Logo ret = new Banner_Logo();
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

                    case "Note":

                        ret.Note = ConvertObject.ToString(dt[column]);
                        break;
                    case "Image":

                        ret.Image = ConvertObject.ToString(dt[column]);
                        break;
                    case "Type":
                        ret.Type = (ValueDefine.BannerLogoType)ConvertObject.ToInt(dt[column].ToString());
                        break;
                    case "Active":
                        ret.Active = ConvertObject.ToBool(dt[column]);
                        break;

                    case "ProductCateID":
                        ret.ProductCateID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "ShowFrom":
                        ret.ShowFrom = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "ShowTo":
                        ret.ShowTo = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
                        break;
                    case "Position":
                        ret.Position = (ValueDefine.BannerLogoPosition)ConvertObject.ToInt(dt[column]);
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }


    }
}
