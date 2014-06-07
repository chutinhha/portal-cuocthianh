using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class Configuration
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Configuration()
        {
            Code = "";
            Name = "";
            Value = "";
        }

        static Configuration DynamicCast<T>(object row_data, object row_header) where T : Configuration
        {
            // row_data : DataRow
            // row_header : DataColumnCollection
            Configuration ret = new Configuration();
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
                    case "Value":
                        ret.Value = ConvertObject.ToString(dt[column]);
                        break;

                    default:
                        break;
                }
            }

            return ret;
        }

    }
}
