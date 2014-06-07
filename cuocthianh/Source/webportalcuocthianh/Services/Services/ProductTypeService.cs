using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class ProductTypeService
    {
       readonly IProductTypeEntity entity;

       public ProductTypeService(IProductTypeEntity entity)
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
                   return entity.Save((CoreData.ProductType)_model, Table.ProductType.ToString());
               }
               else
               {
                   return entity.Update((CoreData.ProductType)_model, Table.ProductType.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.ProductType GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.ProductType.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductType> GetList()
       {
           try
           {
               return entity.GetAll(Table.ProductType.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductType> GetListByLINQ(Func<CoreData.ProductType, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.ProductType.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.ProductType GetOneByLINQ(Func<CoreData.ProductType, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.ProductType.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.ProductType> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.ProductType.ToString()).ToList();
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
               entity.Delete((CoreData.ProductType)_model, Table.ProductType.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
