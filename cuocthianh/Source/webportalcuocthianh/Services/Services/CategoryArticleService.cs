using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class CategoryArticleService
    {
       readonly ICategoryArticleEntity entity;

       public CategoryArticleService(ICategoryArticleEntity entity)
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
               var obj = ((CategoryArticle)_model);
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
                   return entity.Save((CoreData.CategoryArticle)_model, Table.CategoryArticle.ToString());
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
                   return entity.Update((CoreData.CategoryArticle)_model, Table.CategoryArticle.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.CategoryArticle GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.CategoryArticle.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CategoryArticle> GetList()
       {
           try
           {
               return entity.GetAll(Table.CategoryArticle.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CategoryArticle> GetListByLINQ(Func<CoreData.CategoryArticle, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.CategoryArticle.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.CategoryArticle GetOneByLINQ(Func<CoreData.CategoryArticle, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.CategoryArticle.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.CategoryArticle> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.CategoryArticle.ToString()).ToList();
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
               entity.Delete((CoreData.CategoryArticle)_model, Table.CategoryArticle.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method



        #endregion 
    
    }
         
}
