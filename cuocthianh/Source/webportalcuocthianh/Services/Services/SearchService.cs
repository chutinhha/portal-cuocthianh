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
        readonly IExamineeEntity ExamineeEntity;
        public SearchService(IUserEntity entity,IProductEntity product, ICategoryEntity category, IArticleEntity articel, IManufacturerEntity fac, IExamineeEntity exc )
       {
           this.entity = entity;
           this.Productentity = product;
           this.Categoryenity = category;
           this.Articelenity = articel;
           this.ManuFacenity = fac;
           this.ExamineeEntity = exc;
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
                var picexaminee = new List<Examinee>();
                picexaminee = ExamineeEntity.GetMany(
                    c => StringHelper.RemoveVietNamString(c.Description.ToLower()).Contains(StringHelper.RemoveVietNamString(value)) || StringHelper.RemoveVietNamString(c.UserNameExt.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Examinee.ToString()).ToList();
                if (picexaminee != null) { 
                    foreach (var item in picexaminee)
                    {
                        Search s = new Search();
                        s.Name = item.UserNameExt;
                        s.Type = "Nhà sản xuất";
                        s.Image = "/Media/PictureExam/" + item.Image;
                        s.Link = "Examinee/detail/" + item.ID + "?username=" + entity.GetById(item.UserID, Table.Users.ToString()).Name;
                        s.Description = item.Description;
                        lst.Add(s);
                    }
                }
                var article = new List<Article>();
                article = Articelenity.GetMany(c => StringHelper.RemoveVietNamString(c.Title.ToLower()).Contains(StringHelper.RemoveVietNamString(value)), Table.Article.ToString()).ToList();
                if (article != null)
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
