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

    public partial class ManufacturerService
    {
       readonly IManufacturerEntity entity;

       public ManufacturerService(IManufacturerEntity entity)
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
               var obj = ((Manufacturer)_model);
               var id = obj.ID;
               if (id == 0)
               {

                   obj.Code = obj.Name;
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Name);
                   }
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = "noimage.png";
                   }
                   return entity.Save((CoreData.Manufacturer)_model, Table.Manufacturer.ToString());
               }
               else
               {
                   if (obj.Image == null || obj.Image == "")
                   {
                       obj.Image = GetByID(obj.ID).Image;
                   }
                   if (obj.Link == null || obj.Link == "")
                   {
                       obj.Link = StringHelper.GeneratorLink(obj.Name);
                   }
                   return entity.Update((CoreData.Manufacturer)_model, Table.Manufacturer.ToString());
               }
           }
           catch { return -1; }
       }

       /// <summary>
       /// Get by ID
       /// </summary>
       /// <param name="_id"></param>
       /// <returns></returns>
       public CoreData.Manufacturer GetByID(long _id)
       {
           try
           {
               return entity.GetById(_id, Table.Manufacturer.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Manufacturer> GetList()
       {
           try
           {
               return entity.GetAll(Table.Manufacturer.ToString()).ToList();
           }
           catch { return null; }

       }

       /// <summary>
       /// Get List with Linq
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Manufacturer> GetListByLINQ(Func<CoreData.Manufacturer, Boolean> _where)
       {
           try
           {
               return entity.GetMany(_where, Table.Manufacturer.ToString()).ToList();
           }
           catch { return null; }
       }
       /// <summary>
       /// Get One with Linq
       /// </summary>
       /// <returns></returns>
       public CoreData.Manufacturer GetOneByLINQ(Func<CoreData.Manufacturer, Boolean> _where)
       {
           try
           {
               return entity.Get(_where, Table.Manufacturer.ToString());
           }
           catch { return null; }
       }

       /// <summary>
       /// Get List with search string
       /// </summary>
       /// <returns></returns>
       public IList<CoreData.Manufacturer> GetList(string _searchstring)
       {
           //search theo tieu chi nao do         
           try
           {
               return entity.GetBySearchString(_searchstring, Table.Manufacturer.ToString()).ToList();
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
               entity.Delete((CoreData.Manufacturer)_model, Table.Manufacturer.ToString());
               return true;
           }
           catch { return false; }
       }


       #endregion



        #region Other Method


       public List<SelectListItem> GetSelectList(int id)
       {
           var lst = new List<SelectListItem>();
           var listcate = GetList();
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



        #endregion 
    
    }
         
}
