using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IDistrictActionService
    {
        long Save(object _model);
        CoreData.District GetByID(long _id);
        IList<CoreData.District> GetList();
        IList<CoreData.District> GetListByLINQ(Func<CoreData.District, Boolean> _where);
        CoreData.District GetOneByLINQ(Func<CoreData.District, Boolean> _where);
        IList<CoreData.District> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class DistrictActionService:IDistrictActionService
    {
       DistrictService Service;

       public DistrictActionService(DistrictService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.District GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.District> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.District> GetListByLINQ(Func<CoreData.District, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.District GetOneByLINQ(Func<CoreData.District, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.District> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method
        #endregion

    }
         
}
