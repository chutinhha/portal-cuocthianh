using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Helper;
using System.Web.Mvc;
namespace CoreData
{
    public class Product
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CateID { get; set; }
        public long ModelID { get; set; }
        public long ManufacturerID { get; set; }
        public long ProductTypeID { get; set; }
        public long ProductComponenID { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public DateTime DiscountFrom { get; set; }
        public DateTime DiscountTo { get; set; }
        public int Instock { get; set; }
        public DateTime UpdateDate { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public int SoleAmout { get; set; }
        public bool New { get; set; }
        public bool Typical { get; set; }
        
        public string Link { get; set; }
        public bool ShowHomePage { get; set; }
        public string Image { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Active { get; set; }
        public string CategoryNameExt { get; set; }
        public string ModelNameExt { get; set; }
        public string ManufacturerNameExt { get; set; }
        public string ProductTypeNameExt { get; set; }
        public bool IsSendMailExt { get; set; }

        public Category CategoryExt { get; set; }
        public IList<Category> CategoryListExt { get; set; }
        public Manufacturer ManufactuerExt { get; set; }
        public IList<ProductAttribute> ProductAttributeExt { get; set; }

        public List<SelectListItem> ListCategoryExt { get; set; }
        public List<SelectListItem> ListModelExt { get; set; }
        public List<SelectListItem> ListManufacturerExt { get; set; }
        public List<SelectListItem> ListProductTypeExt { get; set; }
        public List<SelectListItem> ListSearchPriceExt { get; set; }
        public List<SelectListItem> ListEmailTemplateExt { get; set; }
        public int EmailTemplateIDExt { get; set; }

        /// <summary>
        /// Pager Variable
        /// </summary>
        public int pageExt { get; set; }
        public int PageSizeExt { get; set; }
        public int TotalPageExt { get; set; }


        public Product()
        {
            Code = "";
            Name = "";
            ShortDescription = "";
            FullDescription = "";
            Link = "";
            Image = "";
            MetaTitle = "";
            MetaKeyword = "";
            MetaDescription = "";
            ModelNameExt = "";
            ManufacturerNameExt = "";
            ProductTypeNameExt = "";
            CategoryNameExt = "";
        }

        static Product DynamicCast<T>(object row_data, object row_header) where T : Product
        {
            // row_data : DataRow
            // row_header : DataColumnCollection
            Product ret = new Product();
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
                    case "CateID":
                        ret.CateID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "ModelID":
                        ret.ModelID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "ProductTypeID":
                        ret.ProductTypeID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "ProductComponenID":
                        ret.ProductComponenID = ConvertObject.ToLong(dt[column]);
                        break;
                    case "ShortDescription":
                        ret.ShortDescription = ConvertObject.ToString(dt[column]);
                        break;
                    case "FullDescription":
                        ret.FullDescription = ConvertObject.ToString(dt[column]);
                        break;
                    case "Price":
                        ret.Price = ConvertObject.ToInt(dt[column]);
                        break;
                    case "Discount":
                        ret.Discount = ConvertObject.ToInt(dt[column]);
                        break;

                    case "DiscountFrom":
                        ret.DiscountFrom = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "DiscountTo":
                        ret.DiscountTo = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "Instock":
                        ret.Instock = ConvertObject.ToInt(dt[column]);
                        break;
                    case "UpdateDate":
                        ret.UpdateDate = ConvertObject.ToDateTime(dt[column]);
                        break;
                    case "View":
                        ret.View = ConvertObject.ToInt(dt[column]);
                        break;
                    case "Like":
                        ret.Like = ConvertObject.ToInt(dt[column]);
                        break;
                    case "SoleAmout":
                        ret.SoleAmout = ConvertObject.ToInt(dt[column]);
                        break;

                    case "Link":
                        ret.Link = ConvertObject.ToString(dt[column]);
                        break;
                    case "ShowHomePage":
                        ret.ShowHomePage = ConvertObject.ToBool(dt[column]);
                        break;
                    case "Image":
                        ret.Image = ConvertObject.ToString(dt[column]);
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
                    case "ModelName":
                        ret.ModelNameExt = ConvertObject.ToString(dt[column]);
                        break;
                    case "ManufacturerName":
                        ret.ManufacturerNameExt = ConvertObject.ToString(dt[column]);
                        break;
                    case "ProductTypeName":
                        ret.ProductTypeNameExt = ConvertObject.ToString(dt[column]);
                        break;
                    case "CategoryName":
                        ret.CategoryNameExt = ConvertObject.ToString(dt[column]);
                        break;
                   
                    case "New":
                        ret.New = ConvertObject.ToBool(dt[column]);
                        break;
                    case "Typical":
                        ret.Typical = ConvertObject.ToBool(dt[column]);
                        break;
                    case "ManufacturerID":
                        ret.ManufacturerID = ConvertObject.ToInt(dt[column]);
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }

    }
}
