using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web.Mvc;
namespace Services
{

    public partial class ProductService
    {
       readonly IProductEntity entity;
       readonly IModelEntity modelentity;
       readonly IProductTypeEntity productentity;
       readonly IManufacturerEntity menufacturerentity;
       readonly ICategoryEntity categoryentity;
       public ProductService(IProductEntity entity,IModelEntity modelentity, IProductTypeEntity producttype,
           IManufacturerEntity manufacturer, ICategoryEntity category)
       {
           this.entity = entity;
           this.modelentity = modelentity;
           this.productentity = producttype;
           this.menufacturerentity = manufacturer;
           this.categoryentity = category;
//bat 1 tien trinh check hang ton kho/thoi han khuyen mai
       }

       #region Main Method

       /// <summary>
       /// Save 
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public long Save(object _model, ref string Link)
       {
           try
           {
               var obj = ((Product)_model);
               var id = obj.ID;
               obj.UpdateDate = DateTime.Now;
               
               if (id == 0)
               {

                   obj.Code = obj.Name;
                  
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Name);
                   }
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = "noimage.png";
                   }
                   Link = obj.Link;
                   return entity.Save((CoreData.Product)_model, Table.Product.ToString());
               }
               else
               {
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = GetByID(obj.ID).Image;
                   }
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Name);
                   }
                   Link = obj.Link;
                   return entity.Update((CoreData.Product)_model, Table.Product.ToString());
               }
              
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Product GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Product.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Product> GetList()
       {
           try
           {
               return entity.GetByCusTomSQL(SQLCommand.GetProductList).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Product> GetListByLINQ(Func<CoreData.Product, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Product.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Product GetOneByLINQ(Func<CoreData.Product, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Product.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Product> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Product.ToString()).ToList();
           }
           catch { return null; }
       }


       /// <summary>
       /// Delete
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public bool Delete(object _model)
       {
           try
           {
               entity.Delete((CoreData.Product)_model, Table.Product.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetListModel(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = modelentity.GetAll(Table.Model.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           return lst;

       }

       public List<SelectListItem> GetListCategory(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = categoryentity.GetAll(Table.Category.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           return lst;

       }

       public List<SelectListItem> GetListProductType(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = productentity.GetAll(Table.ProductType.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           return lst;

       }

       public List<SelectListItem> GetListManufacturer(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = menufacturerentity.GetAll(Table.Manufacturer.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           return lst;

       }
       public List<SelectListItem> GetListSearchPrice(int id)
       {
           var lst = new List<SelectListItem>();
          
           SelectListItem duoi1trieu = new SelectListItem();
           duoi1trieu.Text = "Dưới 1 triệu";
           duoi1trieu.Value = "1";
           lst.Add(duoi1trieu);
           SelectListItem duoi2trieu = new SelectListItem();
           duoi2trieu.Text = "Dưới 2 triệu";
           duoi2trieu.Value = "2";
           lst.Add(duoi2trieu);
           SelectListItem duoi3trieu = new SelectListItem();
           duoi3trieu.Text = "Dưới 3 triệu";
           duoi3trieu.Value = "3";
           lst.Add(duoi3trieu);
           SelectListItem duoi5trieu = new SelectListItem();
           duoi5trieu.Text = "Dưới 5 triệu";
           duoi5trieu.Value = "4";
           lst.Add(duoi5trieu);
           SelectListItem duoi8trieu = new SelectListItem();
           duoi8trieu.Text = "Dưới 8 triệu";
           duoi8trieu.Value = "5";
           lst.Add(duoi8trieu);
           SelectListItem duoi10trieu = new SelectListItem();
           duoi10trieu.Text = "Dưới 10 triệu";
           duoi10trieu.Value = "6";
           lst.Add(duoi10trieu);
           SelectListItem tren10trieu = new SelectListItem();
           tren10trieu.Text = "Trên 10 triệu";
           tren10trieu.Value = "7";
           lst.Add(tren10trieu);
         
           foreach (var c in lst)
           {

               if (id != 0)
               {
                   c.Selected = c.Value == id.ToString();
                   //break;
               }
               
           }
           return lst;

       }

       public long UpdateSoleAmount(int productid, int Amount)
       {
           try {
               var p = entity.GetById((long)productid, Table.Product.ToString());
               p.SoleAmout += Amount;
              return entity.Save(p, Table.Product.ToString());
           }
           catch { return -1; }
       }


       public IList<Product> GetBySearch(string name, string price, string manufacturer,string cateid,string manuid, string type)
       {
           try
           {
               string search ="";
               if(String.IsNullOrEmpty(name)==false)
               {
                   search +=" and Product.Name like N'%" + name + "%'";
               }
               try
               {
                   if (String.IsNullOrEmpty(price) == false && price != "0")
                   {
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi1trieu)
                           search += " and Product.Price < 1000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi2trieu)
                           search += " and Product.Price < 2000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi3trieu)
                           search += " and Product.Price < 3000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi5trieu)
                           search += " and Product.Price < 5000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi8trieu)
                           search += " and Product.Price < 8000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.tren10trieu)
                           search += " and Product.Price > 10000000  ";
                       if (int.Parse(price) == (int)Helper.ValueDefine.SearchPrice.duoi10trieu)
                           search += " and Product.Price < 10000000  ";
                   }
               }
               catch { }
               try
               {
                   if (String.IsNullOrEmpty(manufacturer) == false&&manufacturer!="0")
                   {
                       var test = int.Parse(manufacturer);
                       search += " and Product.ManufacturerID =" + manufacturer;
                   }
               }
               catch { }
               if (type == "Category")
               {
                   search += " and CateID = " + cateid;
               }
               if (type == "Manufac")
               {
                   search += " and Product.ManufacturerID = " + manuid;
               }
              
                return entity.GetByCusTomSQL(String.Format(SQLCommand.SearchProductOnHome, search)).ToList();
           }
           catch { return null; }
       }

       public IList<Product> GetListByCategory(long CatId) {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetListProductByCategoryId, CatId)).ToList();
           }
           catch {
               return null;
           }
       }

        #endregion 
    
    }
         
}
