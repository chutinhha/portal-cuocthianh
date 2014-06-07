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
    public partial class ArticleService
    {
        readonly IArticleEntity entity;
       readonly IUserEntity userentity;
       public ArticleService(IArticleEntity entity, IUserEntity userentity)
       {
           this.entity = entity;
           this.userentity = userentity;
           //bat 1 tien trinh check hang ton kho/thoi han khuyen mai
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
               var obj = ((Article)_model);
               var userid = obj.UserID;
               obj.UpdateDate = DateTime.Now;
               var id = obj.ID;
               if (id == 0)
               {
                   obj.UserID = SessionManagement.GetSessionReturnInt("UserID");
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Title);
                   }
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = "noimage.png";
                   }
                   return entity.Save((CoreData.Article)_model, Table.Article.ToString());
               }
               else
               {
                   obj.UserID = userid;
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = GetByID(obj.ID).Image;
                   }
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Title);
                   }
                   if (obj.Content == null || obj.Content == "")
                   {
                       obj.Link = obj.Content;
                   }
                   return entity.Update((CoreData.Article)_model, Table.Article.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Article GetByID(long _id)
       {
           try
           {
               DictionaryDefine df = new DictionaryDefine();

               var data = entity.GetById(_id, Table.Article.ToString());
               
               foreach (var item in df.DicArticleCategory)
               {
                   if (data.CateID == item.Key)
                    {
                        data.CateID = item.Key;
                        data.CategoryNameExt = item.Value;
                        data.View = data.View + 1;
                        Save(data);
                    }
               }
               return data;
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Article> GetList()
       {
           try
           {
               DictionaryDefine df = new DictionaryDefine();

               var list = entity.GetByCusTomSQL(SQLCommand.GetArticleList).ToList();
               foreach (var item in df.DicArticleCategory)
               {
                   for (int i = 0; i < list.Count; i++)
                   {
                       if (list[i].CateID == item.Key)
                       {
                           list[i].CateID = item.Key;
                           list[i].CategoryNameExt = item.Value;
                       }
                   }
               }
               //return entity.GetByCusTomSQL(SQLCommand.GetArticleList).ToList();
               return list;
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Article> GetListByLINQ(Func<CoreData.Article, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Article.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Article GetOneByLINQ(Func<CoreData.Article, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Article.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Article> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               DictionaryDefine df = new DictionaryDefine();

               var list = entity.GetBySearchString(_searchstring, Table.Article.ToString()).ToList();
               foreach (var item in df.DicArticleCategory)
               {
                   for (int i = 0; i < list.Count; i++)
                   {
                       if (list[i].CateID == item.Key)
                       {
                           list[i].CateID = item.Key;
                           list[i].CategoryNameExt = item.Value;
                       }
                   }
               }
               //return entity.GetBySearchString(_searchstring, Table.Article.ToString()).ToList();
               return list;
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
               entity.Delete((CoreData.Article)_model, Table.Article.ToString());
               return true;
           }
           catch { return false; }
       }

       #endregion



        #region Other Method
        public List<SelectListItem> GetListCategory(int id)
        {
            var lst = new List<SelectListItem>();       
            DictionaryDefine listcate = new DictionaryDefine();
            foreach (var item in listcate.DicArticleCategory) {
                var sitem = new SelectListItem();
                sitem.Value = item.Key.ToString();
                sitem.Text = item.Value;
                lst.Add(sitem); 
            }
            return lst;
        }

        /// <summary>
        /// Get Article by Category ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Article> GetListByRelationsID(long catid, string username)
        {
            try
            {
                string tempstring = "";
                string getuserid = "";
                if (username == "" && catid == 0)
                {
                    tempstring = "";
                }
                else
                {
                    if (username != "" && catid == 0)
                    {
                        tempstring = " and u.UserName LIKE '" + username + "'";
                        getuserid = ", u.Name AS UserName";
                    }
                    if (catid != 0) {
                        if (username == "")
                        {
                            tempstring = " and a.CateID = '" + catid + "'";
                        }
                        else
                        {
                            tempstring = " and u.UserName LIKE '" + username + "'" + " and CateID = '" + catid + "'";
                            getuserid = ", u.Name AS UserName";
                        }   
                    }
                }
                var list = entity.GetByCusTomSQL(String.Format(SQLCommand.GetListByRelationsID,getuserid, tempstring)).ToList();
                DictionaryDefine df = new DictionaryDefine();
                if (catid != 0)
                {
                    foreach (var item in df.DicArticleCategory)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].CateID == item.Key)
                            {
                                list[i].CateID = item.Key;
                                list[i].CategoryNameExt = item.Value;
                            }
                        }
                    }
                }
                return list;
            }
            catch { return null; }
        }
        
        
        /// <summary>
        /// Get the same of Article by Category ID width LIMIT of row
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_rownum"></param>
        /// <returns></returns>
        public List<Article> GetSameListByCateID(long id, int _rownum)
        {
            try
            {
                return entity.GetByCusTomSQL(String.Format(SQLCommand.GetArticleSameListByCateID,id, _rownum)).ToList();
            }
            catch { return null; }
        }
        #endregion 
    }
}
