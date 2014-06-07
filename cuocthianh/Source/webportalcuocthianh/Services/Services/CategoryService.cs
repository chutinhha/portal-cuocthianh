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

    public partial class CategoryService
    {
       readonly ICategoryEntity entity;

       public CategoryService(ICategoryEntity entity)
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
               var obj = ((Category)_model);
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
                   return entity.Save((CoreData.Category)_model, Table.Category.ToString());
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
                   return entity.Update((CoreData.Category)_model, Table.Category.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Category GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Category.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Category> GetList()
       {
           try
           {
               return entity.GetAll(Table.Category.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Category> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Category.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Category> GetListByLINQ(Func<CoreData.Category, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Category.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Category GetOneByLINQ(Func<CoreData.Category, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Category.ToString());
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
               entity.Delete((CoreData.Category)_model, Table.Category.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method
       
        #endregion 


        #region Validate

       public bool ValidateExist(Category _entity, List<Category> _objects, string _propertiesname,out string error)
       {
           var data = _entity.GetType().GetProperty(_propertiesname).GetValue(_entity, null);
           foreach (var item in _objects)
           {
               if (item.ID != _entity.ID)
               {
                   if (item.GetType().GetProperty(_propertiesname).Name == _propertiesname)
                   {
                       if (item.GetType().GetProperty(_propertiesname).GetValue(item, null).ToString()==(data.ToString()))
                       {
                           error = _propertiesname+ " bị trùng";
                           return true;
                       }
                   }
               }
           }
           error = "";
           return false;
       }

        #endregion

    }
         
}
