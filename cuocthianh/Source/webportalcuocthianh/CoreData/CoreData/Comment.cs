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
        public long ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CommentContent { get; set; }
        public long ReferenceID { get; set; }
        public int CommentType { get; set; }
        public long ParentID { get; set; }
        public DateTime PostDate { get; set; }
        public bool Active { get; set; }
        public int UserID { get; set; }

        public Comment(long _ID, int _ReferenceID, int _UserID, int _CommentType, string _CommentContent, DateTime _PostDate, int _parentID)
        {
            this.ID = _ID;
            this.ReferenceID = _ReferenceID;
            this.UserID = _UserID;
            this.CommentType = _CommentType;
            this.CommentContent = _CommentContent;
            this.PostDate = _PostDate;
            this.ParentID = _parentID;
        }
        public Comment()
        {
            Name = "";
            Email = "";
            Phone = "";
            Address = "";
            CommentContent = "";
            //CommentType ="";   
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
                    case "ID":
                        ret.ID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "Name":
                        ret.Name = ConvertObject.ToString(dt[column]);
                        break;
                    case "Email":
                        ret.Email = ConvertObject.ToString(dt[column]);
                        break;
                    case "Phone":
                        ret.Phone = ConvertObject.ToString(dt[column]);
                        break;
                    case "Address":
                        ret.Address = ConvertObject.ToString(dt[column]);
                        break;
                    case "CommentContent":
                        ret.CommentContent = ConvertObject.ToString(dt[column]);
                        break;
                    case "ReferenceID":
                        ret.ReferenceID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "CommentType":
                        ret.CommentType = ConvertObject.ToInt(dt[column]);
                        break;
                    case "ParentID":
                        ret.ParentID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "PostDate":
                        ret.PostDate = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "Active":
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
