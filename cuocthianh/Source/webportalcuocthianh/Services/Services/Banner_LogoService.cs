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

    public partial class Banner_LogoService
    {
       readonly IBanner_LogoEntity entity;
       readonly ICategoryEntity categoryentity;

       public Banner_LogoService(IBanner_LogoEntity entity, ICategoryEntity categoryentity)
       {
           this.entity = entity;
           this.categoryentity = categoryentity;
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
                   return entity.Save((CoreData.Banner_Logo)_model, Table.Banner_Logo.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Banner_Logo)_model, Table.Banner_Logo.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Banner_Logo GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Banner_Logo.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Banner_Logo> GetList()
       {
           try
           {
               return entity.GetAll(Table.Banner_Logo.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Banner_Logo> GetListByLINQ(Func<CoreData.Banner_Logo, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Banner_Logo.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Banner_Logo GetOneByLINQ(Func<CoreData.Banner_Logo, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Banner_Logo.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Banner_Logo> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Banner_Logo.ToString()).ToList();
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
               entity.Delete((CoreData.Banner_Logo)_model, Table.Banner_Logo.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method
        /// <summary>
        /// Get List Category to SelectList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public List<SelectListItem> GetListCategory(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = categoryentity.GetAll(Table.Category.ToString());
           foreach (var c in listcate)
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
        /// <summary>
        /// Get Banner Logo Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public List<SelectListItem> GetListType(int id)
       {
           List<SelectListItem> enumList = new List<SelectListItem>();
           foreach (ValueDefine.BannerLogoType data in Enum.GetValues(typeof(ValueDefine.BannerLogoType)))
           {
                   enumList.Add(new SelectListItem
                   {
                       Text = data.ToString().Replace("_", " "),
                       Value = ((int)Enum.Parse(typeof(ValueDefine.BannerLogoType), data.ToString())).ToString()
                   });
           }

           return enumList;
       }
        /// <summary>
        /// Get Banner Logo Position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public List<SelectListItem> GetListPosition(int id)
       {
           List<SelectListItem> enumList = new List<SelectListItem>();
           foreach (ValueDefine.BannerLogoPosition data in Enum.GetValues(typeof(ValueDefine.BannerLogoPosition)))
           {
               enumList.Add(new SelectListItem
               {
                   Text = data.ToString().Replace("_", " "),
                   Value = ((int)Enum.Parse(typeof(ValueDefine.BannerLogoPosition), data.ToString())).ToString()
               });
           }
           return enumList;
       }
        #endregion 
    
    }
         
}
