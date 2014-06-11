using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface ICategoryArticleActionService
    {
        long Save(object _model);
        CoreData.CategoryArticle GetByID(long _id);
        IList<CoreData.CategoryArticle> GetList();
        IList<CoreData.CategoryArticle> GetListByLINQ(Func<CoreData.CategoryArticle, Boolean> _where);
        CoreData.CategoryArticle GetOneByLINQ(Func<CoreData.CategoryArticle, Boolean> _where);
        IList<CoreData.CategoryArticle> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class CategoryArticleActionService:ICategoryArticleActionService
    {
       CategoryArticleService Service;

       public CategoryArticleActionService(CategoryArticleService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.CategoryArticle GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.CategoryArticle> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.CategoryArticle> GetListByLINQ(Func<CoreData.CategoryArticle, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.CategoryArticle GetOneByLINQ(Func<CoreData.CategoryArticle, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.CategoryArticle> GetList(string _searchstring)
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
