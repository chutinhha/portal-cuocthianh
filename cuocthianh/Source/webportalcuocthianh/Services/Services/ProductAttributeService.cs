using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class ProductAttributeService
    {
       readonly IProductAttributeEntity entity;

       public ProductAttributeService(IProductAttributeEntity entity)
       {
           this.entity = entity;

       }

       #region Main Method

       /// <summary>
       /// Save 
       /// </summary>
       /// <param name="_model"></param>
       /// <returns></returns>
       public long Save(object _model)
       {
           try
           {
               var id = long.Parse(_model.GetType().GetProperty("ID").GetValue(_model, null).ToString());
               if (id == 0)
               {
                   return entity.Save((CoreData.ProductAttribute)_model, Table.ProductAttribute.ToString());
               }
               else
               {
                   return entity.Update((CoreData.ProductAttribute)_model, Table.ProductAttribute.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.ProductAttribute GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.ProductAttribute.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductAttribute> GetList()
       {
           try
           {
               return entity.GetAll(Table.ProductAttribute.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductAttribute> GetListByLINQ(Func<CoreData.ProductAttribute, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.ProductAttribute.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.ProductAttribute GetOneByLINQ(Func<CoreData.ProductAttribute, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.ProductAttribute.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductAttribute> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.ProductAttribute.ToString()).ToList();
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
               entity.Delete((CoreData.ProductAttribute)_model, Table.ProductAttribute.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductAttribute> GetListByProductID(long _productID)
       {
           try
           {
               return entity.GetByCusTomSQL(String.Format(SQLCommand.GetListProductAttribute,_productID)).ToList();
           }
           catch { return null; }

       }


        #endregion 
    
    }
         
}
