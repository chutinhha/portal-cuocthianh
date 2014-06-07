using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class ModelService
    {
       readonly IModelEntity entity;

       public ModelService(IModelEntity entity)
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
               var obj = ((Model)_model);
               var id = obj.ID;
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
                   return entity.Save((CoreData.Model)_model, Table.Model.ToString());
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
                   return entity.Update((CoreData.Model)_model, Table.Model.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Model GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Model.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Model> GetList()
       {
           try
           {
              // return entity.GetAll(Table.Model.ToString()).ToList();
               return entity.GetByCusTomSQL(SQLCommand.GetAllWithManufacturerName).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Model> GetListByLINQ(Func<CoreData.Model, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Model.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Model GetOneByLINQ(Func<CoreData.Model, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Model.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Model> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Model.ToString()).ToList();
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
               entity.Delete((CoreData.Model)_model, Table.Model.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
