using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IConfigurationActionService
    {
        long Save(object _model);
        CoreData.Configuration GetByID(long _id);
        IList<CoreData.Configuration> GetList();
        IList<CoreData.Configuration> GetListByLINQ(Func<CoreData.Configuration, Boolean> _where);
        CoreData.Configuration GetOneByLINQ(Func<CoreData.Configuration, Boolean> _where);
        IList<CoreData.Configuration> GetList(string _searchstring);
        bool Delete(object _model);
        IList<Configuration> GetGenneralConfig();
        long UpdateConfig(string data);
        IList<Configuration> GetEmailConfig();
    }

    public partial class ConfigurationActionService:IConfigurationActionService
    {
       ConfigurationService Service;

       public ConfigurationActionService(ConfigurationService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Configuration GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Configuration> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Configuration> GetListByLINQ(Func<CoreData.Configuration, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Configuration GetOneByLINQ(Func<CoreData.Configuration, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Configuration> GetList(string _searchstring)
       {
           return Service.GetList(_searchstring);
       }

       public virtual bool Delete(object _model)
       {
           return Service.Delete(_model);
       }

       #endregion


        #region Other Method

       public virtual IList<Configuration> GetGenneralConfig()
       {
           return Service.GetGenneralConfig();
       }

       public virtual IList<Configuration> GetEmailConfig()
       {
           return Service.GetEmailConfig();
       }

       public virtual long UpdateConfig(string data)
       {
           return Service.UpdateConfig(data);
       }

        #endregion

    }
         
}
