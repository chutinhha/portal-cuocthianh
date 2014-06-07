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

    public interface IManufacturerActionService
    {
        long Save(object _model);
        CoreData.Manufacturer GetByID(long _id);
        IList<CoreData.Manufacturer> GetList();
        IList<CoreData.Manufacturer> GetListByLINQ(Func<CoreData.Manufacturer, Boolean> _where);
        CoreData.Manufacturer GetOneByLINQ(Func<CoreData.Manufacturer, Boolean> _where);
        IList<CoreData.Manufacturer> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetSelectList(int id);
    }

    public partial class ManufacturerActionService:IManufacturerActionService
    {
       ManufacturerService Service;

       public ManufacturerActionService(ManufacturerService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Manufacturer GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Manufacturer> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Manufacturer> GetListByLINQ(Func<CoreData.Manufacturer, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Manufacturer GetOneByLINQ(Func<CoreData.Manufacturer, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Manufacturer> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetSelectList(int id)
       {
           return Service.GetSelectList(id);
       }

        #endregion

    }
         
}
