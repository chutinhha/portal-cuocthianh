using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
namespace Services
{

    public partial class CommentService
    {
       readonly ICommentEntity entity;

       public CommentService(ICommentEntity entity)
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
                   return entity.Save((CoreData.Comment)_model, Table.Comment.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Comment)_model, Table.Comment.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Comment GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Comment.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Comment> GetList()
       {
           try
           {
               return entity.GetAll(Table.Comment.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Comment> GetListByLINQ(Func<CoreData.Comment, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Comment.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Comment GetOneByLINQ(Func<CoreData.Comment, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Comment.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Comment> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Comment.ToString()).ToList();
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
               entity.Delete((CoreData.Comment)_model, Table.Comment.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method
        /// <summary>
        /// Get comment list by type and referenceID
        /// Type define: 0 is Artice, 1 is PictureExam
        /// ReferenceID define: It's id of record into article table or PictureExam table
        /// </summary>
        /// <param name="type"></param>
        /// <param name="referenceid"></param>
        /// <returns></returns>
        public virtual IList<Comment> GetCommentsByTypeAndReferenceID(int type, int referenceid)
        {
            try
            {
                return entity.GetMany(c => c.CommentType.Equals(type) && c.ReferenceID.Equals(referenceid), Table.Comment.ToString()).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generate comment box for picture exam with html code
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateCommentBoxPictureExam(int referenceid)
        {
            var data = GetCommentsByTypeAndReferenceID(1, referenceid);
            return "";
        }





        /// <summary>
        /// Generate comment box for article with html code
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateCommentBoxArticle(int referenceid)
        {
            var data = GetCommentsByTypeAndReferenceID(0, referenceid);
            return "";
        }


        #endregion 
    
    }
         
}