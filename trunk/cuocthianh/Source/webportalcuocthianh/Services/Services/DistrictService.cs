using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class DistrictService
    {
       readonly IDistrictEntity entity;

       public DistrictService(IDistrictEntity entity)
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
                   return entity.Save((CoreData.District)_model, Table.District.ToString());
               }
               else
               {
                   return entity.Update((CoreData.District)_model, Table.District.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.District GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.District.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.District> GetList()
       {
           try
           {
              // return entity.GetAll(Table.District.ToString()).ToList();
               return entity.GetByCusTomSQL(SQLCommand.GetListDistrict).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.District> GetListByLINQ(Func<CoreData.District, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.District.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.District GetOneByLINQ(Func<CoreData.District, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.District.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.District> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.District.ToString()).ToList();
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
               entity.Delete((CoreData.District)_model, Table.District.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       /// <summary>
       /// Get User By Group
       /// </summary>
       /// <param name="_groupid"></param>
       /// <returns></returns>
       public IList<CoreData.District> GetByProvince(int _proid)
       {
           try
           {
               return entity.GetByCusTomSQL(string.Format(SQLCommand.GetListDicTrictByProvince, _proid)).ToList();
           }
           catch { return null; }

       }

        #endregion 
    
    }
         
}
