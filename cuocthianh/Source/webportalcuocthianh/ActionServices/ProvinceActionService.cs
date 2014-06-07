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

    public interface IProvinceActionService
    {
        long Save(object _model);
        CoreData.Province GetByID(long _id);
        IList<CoreData.Province> GetList();
        IList<CoreData.Province> GetListByLINQ(Func<CoreData.Province, Boolean> _where);
        CoreData.Province GetOneByLINQ(Func<CoreData.Province, Boolean> _where);
        IList<CoreData.Province> GetList(string _searchstring);
        bool Delete(object _model);
        List<SelectListItem> GetListProvince(int id);
    }

    public partial class ProvinceActionService:IProvinceActionService
    {
       ProvinceService Service;

       public ProvinceActionService(ProvinceService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Province GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Province> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Province> GetListByLINQ(Func<CoreData.Province, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Province GetOneByLINQ(Func<CoreData.Province, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Province> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual List<SelectListItem> GetListProvince(int id)
       {
           return Service.GetListProvince(id);
       }

        #endregion

    }
         
}
