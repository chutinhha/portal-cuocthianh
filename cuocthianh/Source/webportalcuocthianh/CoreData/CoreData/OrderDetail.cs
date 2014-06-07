using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
namespace CoreData
{
    public class OrderDetail
    {
            public long ID{get;set;}
            public long ProductID{get;set;}
            public long OrderID{get;set;}
            public int Price{get;set;}
            public int Amount{get;set;}
            public int Total{get;set;}
            public string ProductNameExt { get; set; }
            public string ProductLinkExt { get; set; }

            public string NameExt { get; set; }
            public string PhoneExt { get; set; }
            public string AddressExt { get; set; }
            public DateTime CreateDateExt { get; set; }
         
        public OrderDetail()
        {
            ProductNameExt = "";
            NameExt = "";
            PhoneExt = "";
            AddressExt = "";
            ProductLinkExt = "";
        }
        
       static OrderDetail DynamicCast<T>(object row_data, object row_header) where T : OrderDetail
       {
           // row_data : DataRow
           // row_header : DataColumnCollection
           OrderDetail ret = new OrderDetail();
           DataRow dt = (DataRow)row_data;

           foreach (DataColumn column in (DataColumnCollection)row_header)
           {
               switch (column.ColumnName)
               {
                   case "ID":
                       ret.ID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "ProductID":
                       ret.ProductID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "OrderID":
                       ret.OrderID = ConvertObject.ToLong(dt[column]);
                       break;
                   case "Price":
                       ret.Price = ConvertObject.ToInt(dt[column]);
                       break;
                   case "Amount":
                       ret.Amount = ConvertObject.ToInt(dt[column]);
                       break;
                   case "Total":
                       ret.Total = ConvertObject.ToInt(dt[column]);
                       break;
                   case "ProductName":
                       ret.ProductNameExt = ConvertObject.ToString(dt[column].ToString());
                       break;
                   case "ProductLink":
                       ret.ProductLinkExt = ConvertObject.ToString(dt[column].ToString());
                       break;
                   case "Name":
                       ret.NameExt = ConvertObject.ToString(dt[column].ToString());
                       break;
                   case "Phone":
                       ret.PhoneExt = ConvertObject.ToString(dt[column].ToString());
                       break;
                   case "Address":
                       ret.AddressExt = ConvertObject.ToString(dt[column].ToString());
                       break;
                   case "CreateDate":
                       ret.CreateDateExt = ConvertObject.ToDateTime(dt[column].ToString());
                       break;
                   default:
                       break;
               }
           }

           return ret;
       }
        
    }
}
