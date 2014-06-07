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

    public partial class EmailTemplateService
    {
       readonly IEmailTemplateEntity entity;

       public EmailTemplateService(IEmailTemplateEntity entity)
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
                   return entity.Save((CoreData.EmailTemplate)_model, Table.EmailTemplate.ToString());
               }
               else
               {
                   return entity.Update((CoreData.EmailTemplate)_model, Table.EmailTemplate.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.EmailTemplate GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.EmailTemplate.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailTemplate> GetList()
       {
           try
           {
               return entity.GetAll(Table.EmailTemplate.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailTemplate> GetListByLINQ(Func<CoreData.EmailTemplate, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.EmailTemplate.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.EmailTemplate GetOneByLINQ(Func<CoreData.EmailTemplate, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.EmailTemplate.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.EmailTemplate> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.EmailTemplate.ToString()).ToList();
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
               entity.Delete((CoreData.EmailTemplate)_model, Table.EmailTemplate.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetListEmailTemplate()
       {
           var lst = new List<SelectListItem>();
           var listcate = entity.GetAll(Table.EmailTemplate.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
              
               lst.Add(item);
           }
           return lst;

       }

        #endregion 
    
    }
         
}
