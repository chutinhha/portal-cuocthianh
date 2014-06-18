using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Examinee
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Description { get; set; }
        public string UserNameExt { get; set; }
        public int View { get; set; }
        public Examinee()
        {
            Code = "";
            Image = "";
            Link = "";
            Description = "";
        }

        static Examinee DynamicCast<T>(object row_data, object row_header) where T : Examinee
        {
            // row_data : DataRow
            // row_header : DataColumnCollection
            Examinee ret = new Examinee();
            DataRow dt = (DataRow)row_data;

            foreach (DataColumn column in (DataColumnCollection)row_header)
            {
                switch (column.ColumnName)
                {
                    case "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "UserID":
                        ret.UserID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "Code":
                        ret.Code = ConvertObject.ToString(dt[column]);
                        break;
                    case "Image":
                        ret.Image = ConvertObject.ToString(dt[column]);
                        break;
                    case "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
                        break;
                    case "DayOfBirth":
                        ret.DayOfBirth = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "Description":
                        ret.Description = ConvertObject.ToString(dt[column]);
                        break;
                    case "View":
                        ret.View = ConvertObject.ToInt(dt[column]);
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }

    }
}
