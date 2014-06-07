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

    public partial class AttributeService
    {
       readonly IAttributeEntity entity;

       public AttributeService(IAttributeEntity entity)
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
                   return entity.Save((CoreData.Attribute)_model, Table.Attribute.ToString());
               }
               else
               {
                   return entity.Update((CoreData.Attribute)_model, Table.Attribute.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Attribute GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Attribute.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Attribute> GetList()
       {
           try
           {
               return entity.GetAll(Table.Attribute.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Attribute> GetListByLINQ(Func<CoreData.Attribute, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Attribute.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Attribute GetOneByLINQ(Func<CoreData.Attribute, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Attribute.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Attribute> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Attribute.ToString()).ToList();
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
               entity.Delete((CoreData.Attribute)_model, Table.Attribute.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method

       public List<SelectListItem> GetListAttribute(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = entity.GetAll(Table.Attribute.ToString());
           foreach (var c in listcate)
           {
               var item = new SelectListItem();
               item.Text = c.Name;
               item.Value = c.ID.ToString();
               if (id != 0)
                   item.Selected = c.ID == id;
               lst.Add(item);
           }
           lst.Insert(0,new SelectListItem(){ Value="0", Text="Chọn một thuộc tính",Selected =true});
           return lst;

       }


        #endregion 
    
    }
         
}
