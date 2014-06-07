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

    public partial class ProvinceService
    {
       readonly IProvinceEntity entity;

       public ProvinceService(IProvinceEntity entity)
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
                   return entity.Save((CoreData.Province)_model, Table.Province.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Province)_model, Table.Province.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Province GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Province.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Province> GetList()
       {
           try
           {
               return entity.GetAll(Table.Province.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Province> GetListByLINQ(Func<CoreData.Province, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Province.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Province GetOneByLINQ(Func<CoreData.Province, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Province.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Province> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Province.ToString()).ToList();
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
               entity.Delete((CoreData.Province)_model, Table.Province.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetListProvince(int id)
       {
           var lst = new List<SelectListItem>();
           var listprovince = GetList();
           foreach (var c in listprovince)
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

        #endregion 
    
    }
         
}
