using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using Services;
using System.Web.Mvc;

namespace ActionServices
{
    public interface ISearchActionService
    {
        IList<User> SearchUser(string name, string username, string email, string groupid);
        IList<Product> SearchProduct(string code, string name, string producttype, string manufacturer, string category);
        IList<Search> SearchAll(string value, long catId);
        List<SelectListItem> GetSelectListCategory();
    }
   public class SearchActionService:ISearchActionService
    {
        SearchService Service;

       public SearchActionService(SearchService _Service)
       {
           Service = _Service;
       }

       public virtual IList<User> SearchUser(string name, string username, string email, string groupid)
       {
           return Service.SearchUser(name, username, email, groupid);
       }

       public virtual IList<Product> SearchProduct(string code, string name, string producttype, string manufacturer, string category)
       {
           return Service.SearchProduct(code, name, producttype, manufacturer, category);
       }

       public virtual IList<Search> SearchAll(string value, long catId)
       {
           return Service.SearchAll(value, catId);
       }

       public virtual List<SelectListItem> GetSelectListCategory() {
           return Service.GetSelectListCategory();
       }

    }
}
