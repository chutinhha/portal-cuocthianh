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

    public interface IBanner_LogoActionService
    {
        long Save(object _model);
        CoreData.Banner_Logo GetByID(long _id);
        IList<CoreData.Banner_Logo> GetList();
        IList<CoreData.Banner_Logo> GetListByLINQ(Func<CoreData.Banner_Logo, Boolean> _where);
        CoreData.Banner_Logo GetOneByLINQ(Func<CoreData.Banner_Logo, Boolean> _where);
        IList<CoreData.Banner_Logo> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListCategory(int id);
        List<SelectListItem> GetListType(int id);
        List<SelectListItem> GetListPosition(int id);
    }

    public partial class Banner_LogoActionService:IBanner_LogoActionService
    {
       Banner_LogoService Service;

       public Banner_LogoActionService(Banner_LogoService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Banner_Logo GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Banner_Logo> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Banner_Logo> GetListByLINQ(Func<CoreData.Banner_Logo, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Banner_Logo GetOneByLINQ(Func<CoreData.Banner_Logo, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Banner_Logo> GetList(string _searchstring)
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

       public virtual List<SelectListItem> GetListType(int id)
       {
           return Service.GetListType(id);
       }

       public virtual List<SelectListItem> GetListPosition(int id)
       {
           return Service.GetListPosition(id);
       }
        #endregion

    }
         
}
