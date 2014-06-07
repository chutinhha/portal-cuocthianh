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

    public interface IProductAttributeActionService
    {
        long Save(object _model);
        CoreData.ProductAttribute GetByID(long _id);
        IList<CoreData.ProductAttribute> GetList();
        IList<CoreData.ProductAttribute> GetListByLINQ(Func<CoreData.ProductAttribute, Boolean> _where);
        CoreData.ProductAttribute GetOneByLINQ(Func<CoreData.ProductAttribute, Boolean> _where);
        IList<CoreData.ProductAttribute> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListAttribute(int id);
        IList<CoreData.ProductAttribute> GetListByProductID(long _productID);
    }

    public partial class ProductAttributeActionService:IProductAttributeActionService
    {
       ProductAttributeService Service;
       AttributeService AttributeService;
       public ProductAttributeActionService(ProductAttributeService _Service, AttributeService _AttributeService)
       {
           Service = _Service;
           this.AttributeService = _AttributeService;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.ProductAttribute GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.ProductAttribute> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.ProductAttribute> GetListByLINQ(Func<CoreData.ProductAttribute, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.ProductAttribute GetOneByLINQ(Func<CoreData.ProductAttribute, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.ProductAttribute> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetListAttribute(int id)
       {
           return AttributeService.GetListAttribute(id);
       }

       public virtual IList<CoreData.ProductAttribute> GetListByProductID(long _productID)
       {
           return Service.GetListByProductID(_productID);
       }


        #endregion

    }
         
}
