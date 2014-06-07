using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
using System.Web.Mvc;
namespace ActionServices
{

    public interface IProductActionService
    {
        long Save(object _model, ref string Link);
        CoreData.Product GetByID(long _id);
        IList<CoreData.Product> GetList();
        IList<CoreData.Product> GetListByLINQ(Func<CoreData.Product, Boolean> _where);
        CoreData.Product GetOneByLINQ(Func<CoreData.Product, Boolean> _where);
        IList<CoreData.Product> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListModel(int id);
        List<SelectListItem> GetListCategory(int id);
        List<SelectListItem> GetListProductType(int id);
        List<SelectListItem> GetListManufacturer(int id);
        IList<Product> GetBySearch(string name, string price, string manufacturer, string cateid,string manuid, string type);
        long UpdateSoleAmount(int productid, int Amount);
        List<SelectListItem> GetListSearchPrice(int id);
        IList<Product> GetListByCategory(int CatId);
    }

    public partial class ProductActionService:IProductActionService
    {
       ProductService Service;
  
       public ProductActionService(ProductService _Service)
       {
           this.Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model, ref string Link)
       {
           Link = "";
           return Service.Save(_model, ref Link);
       }

       public virtual CoreData.Product GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Product> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Product> GetListByLINQ(Func<CoreData.Product, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Product GetOneByLINQ(Func<CoreData.Product, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Product> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetListModel(int id)
       {
           return Service.GetListModel(id);

       }

       public virtual List<SelectListItem> GetListCategory(int id)
       {
           return Service.GetListCategory(id);

       }

       public virtual List<SelectListItem> GetListProductType(int id)
       {
           return Service.GetListProductType(id);

       }

       public virtual List<SelectListItem> GetListManufacturer(int id)
       {
           return Service.GetListManufacturer(id);

       }
       public virtual List<SelectListItem> GetListSearchPrice(int id)
       {
           return Service.GetListSearchPrice(id);
       }
       public virtual long UpdateSoleAmount(int productid, int Amount)
       {
           return Service.UpdateSoleAmount(productid, Amount);
       }


       public virtual IList<Product> GetBySearch(string name, string price, string manufacturer, string cateid,string manuid, string type)
       {
           return Service.GetBySearch(name, price, manufacturer, cateid,manuid, type);
       }

       public virtual IList<Product> GetListByCategory(int CatId) {
           return Service.GetListByCategory(CatId);
       }
        #endregion

    }
         
}
