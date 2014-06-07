using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web.Mvc;
using System.Web;
namespace Services
{
    public partial class SearchService
    {

        readonly IUserEntity entity;
        readonly IProductEntity Productentity;
        readonly ICategoryEntity Categoryenity;
        readonly IManufacturerEntity ManuFacenity;
        readonly IArticleEntity Articelenity;
        public SearchService(IUserEntity entity,IProductEntity product, ICategoryEntity category, IArticleEntity articel, IManufacturerEntity fac )
       {
           this.entity = entity;
           this.Productentity = product;
           this.Categoryenity = category;
           this.Articelenity = articel;
           this.ManuFacenity = fac;
       }

        public IList<User> SearchUser(string name, string username, string email, string groupid)
        {
            try {
          
                if(!string.IsNullOrEmpty(name))
                    name = " and Name like N'%" + name + "%'";
                if(!string.IsNullOrEmpty(username))
                    username = " and UserName like N'%" + username + "%'";
                if(!string.IsNullOrEmpty(email))
                    email = " and Email like N'%" + email + "%'";           
                return entity.GetByCusTomSQL(string.Format(SQLCommand.SearchUser,groupid,name,username,email)).ToList();
            }
            catch { return null; }
        }
        public IList<Product> SearchProduct(string code, string name, string producttype, string manufacturer, string category)
        {
            try
            {
                string search = "";
                if (!string.IsNullOrEmpty(code))
                    search += " and Product.Code like N'%" + code + "%'";
                if (!string.IsNullOrEmpty(name))
                    search += " and Product.Name like N'%" + name + "%'";
                if (!string.IsNullOrEmpty(producttype))
                    search += " and Product.ProductTypeID =" + producttype;
                if (!string.IsNullOrEmpty(manufacturer))
                    search += " and Product.ManufacturerID =" + manufacturer;
                if (!string.IsNullOrEmpty(category))
                    search += " and Product.CateID =" + category;
                return Productentity.GetByCusTomSQL(string.Format(SQLCommand.SearchProduct, search)).ToList();
            }
            catch { return null; }
        }

        public IList<Search> SearchAll(string value, long catId)
        {
            try {
                IList<Search> lst = new List<Search>();
                List<Product> product = new List<Product>();
                if (catId != 0)
                {
                    product = Productentity.GetMany(c => StringHelper.RemoveVietNamString(c.Name.ToLower()).Contains(StringHelper.RemoveVietNamString(value)) && c.CateID == catId, Table.Product.ToString()).ToList();
                }
                else {
                    product = Productentity.GetMany(c => StringHelper.RemoveVietNamString(c.Name.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Product.ToString()).ToList();
                }
                if(product!=null)
                {
                    foreach (var item in product)
                    {
                        Search s = new Search();
                        var CategoryName = Categoryenity.GetById(item.CateID, Table.Category.ToString()).Name;
                        s.Type = CategoryName;
                        s.Name = item.Name;
                        s.Image ="/Media/Product/" + item.Image;
                        s.Link = item.Link + "-p" + item.ID + ".html";
                        s.Description = item.ShortDescription;
                        lst.Add(s);
                    }
                }
                List<Category> category = new List<Category>();
                if (catId != 0 && product.Count() < 5)
                {
                    category = Categoryenity.GetMany(c => StringHelper.RemoveVietNamString(c.Name.ToLower()).Contains(StringHelper.RemoveVietNamString(value)) && c.ID == catId, Table.Category.ToString()).ToList();
                }
                else if(product.Count() < 5){
                    category = Categoryenity.GetMany(c => StringHelper.RemoveVietNamString(c.Name.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Category.ToString()).ToList();
                }
                if (category != null)
                {
                    foreach (var item in category)
                    {
                        Search s = new Search();
                        s.Type = "Danh mục";
                        s.Name = item.Name;
                        s.Image = "/Media/Category/"+ item.Image;
                        s.Link =   item.Link + "-c" + item.ID + ".html";
                        s.Description = item.Note;
                        lst.Add(s);
                    }
                }
                var Manufac = new List<Manufacturer>();
                if (product.Count() < 5 && category.Count() < 5)
                {
                    Manufac = ManuFacenity.GetMany(c => StringHelper.RemoveVietNamString(c.Name.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Manufacturer.ToString()).ToList();
                }
                if (Manufac != null)
                {
                    foreach (var item in Manufac)
                    {
                        Search s = new Search();
                        s.Name = item.Name;
                        s.Type = "Nhà sản xuất";
                        s.Image = "/Media/Manufacturer/" + item.Image;
                        s.Link = item.Link + "-m" + item.ID + ".html";
                        s.Description = item.Note;
                        lst.Add(s);
                    }
                }
                var article = new List<Article>();
                if (product.Count() < 5 && category.Count() < 5 && Manufac.Count() > 0)
                {
                    article = Articelenity.GetMany(c => StringHelper.RemoveVietNamString(c.Title.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Article.ToString()).ToList();
                }
                if (article != null && Manufac.Count() < 5 && product.Count() < 5 && category.Count() < 5)
                {
                    foreach (var item in article)
                    {
                        Search s = new Search();
                        s.Name = item.Title;
                        s.Type = "Bài viết";
                        s.Image = "/Media/Article/" + item.Image;
                        s.Link = "article/detail/" + item.ID;
                        s.Description = item.ShortDescription;
                        lst.Add(s);
                    }
                }
                return lst;
            }
            catch {
                return null;
            }
        }

        public List<SelectListItem> GetSelectListCategory()
        {
            var lst = new List<SelectListItem>();

            var listcate = Categoryenity.GetMany(x => x.Active == true && x.ParentID == 0, Table.Category.ToString());
            lst.Add(new SelectListItem
            {
                Text = "Tất cả",
                Value = "0"
            });
            foreach (var c in listcate)
            {
                var item = new SelectListItem();
                item.Text = c.Name;
                item.Value = c.ID.ToString();
                lst.Add(item);
            }
            return lst;
        }
    }
}
