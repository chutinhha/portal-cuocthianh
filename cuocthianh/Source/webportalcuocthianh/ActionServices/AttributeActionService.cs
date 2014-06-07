using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using Helper;
namespace ActionServices
{

    public interface IAttributeActionService
    {
        long Save(object _model);
        CoreData.Attribute GetByID(long _id);
        IList<CoreData.Attribute> GetList();
        IList<CoreData.Attribute> GetListByLINQ(Func<CoreData.Attribute, Boolean> _where);
        CoreData.Attribute GetOneByLINQ(Func<CoreData.Attribute, Boolean> _where);
        IList<CoreData.Attribute> GetList(string _searchstring);
        bool Delete(object _model);
    }

    public partial class AttributeActionService:IAttributeActionService
    {
       AttributeService Service;

       public AttributeActionService(AttributeService _Service)
       {
           Service = _Service;
       }

       #region Main Method

       public virtual long Save(object _model)
       {
           return Service.Save(_model);
       }

       public virtual CoreData.Attribute GetByID(long _id)
       {
           return Service.GetByID(_id);
       }

       public virtual IList<CoreData.Attribute> GetList()
       {
           return Service.GetList();
       }

       public virtual IList<CoreData.Attribute> GetListByLINQ(Func<CoreData.Attribute, Boolean> _where)
       {
           return Service.GetListByLINQ(_where);
       }

       public virtual CoreData.Attribute GetOneByLINQ(Func<CoreData.Attribute, Boolean> _where)
       {
           return Service.GetOneByLINQ(_where);
       }

       public virtual IList<CoreData.Attribute> GetList(string _searchstring)
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
