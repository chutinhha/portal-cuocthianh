using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface ICommentActionService
    {
        long Save(object _model);
        CoreData.Comment GetByID(long _id);
        IList<CoreData.Comment> GetList();
        IList<CoreData.Comment> GetListByLINQ(Func<CoreData.Comment, Boolean> _where);
        CoreData.Comment GetOneByLINQ(Func<CoreData.Comment, Boolean> _where);
        IList<CoreData.Comment> GetList(string _searchstring);
        bool Delete(object _model);
        string GenerateCommentBoxPictureExam(int referenceid);
        string GenerateCommentBoxArticle(int referenceid);
       
    }

    public partial class CommentActionService:ICommentActionService
    {
       CommentService Service;

       public CommentActionService(CommentService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Comment GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Comment> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Comment> GetListByLINQ(Func<CoreData.Comment, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Comment GetOneByLINQ(Func<CoreData.Comment, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Comment> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


       #region Other Method

       /// <summary>
       /// Generate comment box for picture exam with html code
       /// </summary>
       /// <returns></returns>
       public virtual string GenerateCommentBoxPictureExam(int referenceid)
       {
           var data = Service.GetCommentsByTypeAndReferenceID(1, referenceid);
           return "";
       }

       /// <summary>
       /// Generate comment box for article with html code
       /// </summary>
       /// <returns></returns>
       public virtual string GenerateCommentBoxArticle(int referenceid)
       {
           var data = Service.GetCommentsByTypeAndReferenceID(0, referenceid);
           return "";
       }

        #endregion

    }
         
}
