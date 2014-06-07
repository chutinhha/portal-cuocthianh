using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IProductTypeActionService
    {
        long Save(object _model);
        CoreData.ProductType GetByID(long _id);
        IList<CoreData.ProductType> GetList();
        IList<CoreData.ProductType> GetListByLINQ(Func<CoreData.ProductType, Boolean> _where);
        CoreData.ProductType GetOneByLINQ(Func<CoreData.ProductType, Boolean> _where);
        IList<CoreData.ProductType> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class ProductTypeActionService:IProductTypeActionService
    {
       ProductTypeService Service;

       public ProductTypeActionService(ProductTypeService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.ProductType GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.ProductType> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.ProductType> GetListByLINQ(Func<CoreData.ProductType, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.ProductType GetOneByLINQ(Func<CoreData.ProductType, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.ProductType> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
        #endregion

    }
         
}
