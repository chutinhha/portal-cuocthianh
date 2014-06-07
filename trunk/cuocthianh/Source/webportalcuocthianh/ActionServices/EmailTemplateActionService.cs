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

    public interface IEmailTemplateActionService
    {
        long Save(object _model);
        CoreData.EmailTemplate GetByID(long _id);
        IList<CoreData.EmailTemplate> GetList();
        IList<CoreData.EmailTemplate> GetListByLINQ(Func<CoreData.EmailTemplate, Boolean> _where);
        CoreData.EmailTemplate GetOneByLINQ(Func<CoreData.EmailTemplate, Boolean> _where);
        IList<CoreData.EmailTemplate> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListEmailTemplate();
    }

    public partial class EmailTemplateActionService:IEmailTemplateActionService
    {
       EmailTemplateService Service;

       public EmailTemplateActionService(EmailTemplateService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.EmailTemplate GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.EmailTemplate> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.EmailTemplate> GetListByLINQ(Func<CoreData.EmailTemplate, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.EmailTemplate GetOneByLINQ(Func<CoreData.EmailTemplate, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.EmailTemplate> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetListEmailTemplate()
       {
           return Service.GetListEmailTemplate();
       }


        #endregion

    }
         
}
