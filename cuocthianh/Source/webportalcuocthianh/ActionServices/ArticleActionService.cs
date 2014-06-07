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
    public interface IArticleActionService
    {
        long Save(object _model);
        CoreData.Article GetByID(long _id);
        IList<CoreData.Article> GetList();
        IList<CoreData.Article> GetListByLINQ(Func<CoreData.Article, Boolean> _where);
        CoreData.Article GetOneByLINQ(Func<CoreData.Article, Boolean> _where);
        IList<CoreData.Article> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListCategory(int id);
        IList<CoreData.Article> GetListByRelationsID(long id, string username);
        List<CoreData.Article> GetSameListByCateID(long id, int _rownum);
    }
    public partial class ArticleActionService : IArticleActionService
    {
        ArticleService Service;

        public ArticleActionService(ArticleService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Article GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Article> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Article> GetListByLINQ(Func<CoreData.Article, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Article GetOneByLINQ(Func<CoreData.Article, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Article> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
         
       }

       #endregion


        #region Other Method
       public virtual List<SelectListItem> GetListCategory(int id)
       {
           return Service.GetListCategory(id);
       }
        /// <summary>
        /// Get Article by Category ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public virtual IList<CoreData.Article> GetListByRelationsID(long catid, string username)
       {
           return Service.GetListByRelationsID(catid, username);
       }
        /// <summary>
       /// Get the same of Article by Category ID width LIMIT of row
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_rownum"></param>
        /// <returns></returns>
       public virtual List<CoreData.Article> GetSameListByCateID(long id, int _rownum)
       {
           return Service.GetSameListByCateID(id, _rownum);
       }
        #endregion
    
    }
}
