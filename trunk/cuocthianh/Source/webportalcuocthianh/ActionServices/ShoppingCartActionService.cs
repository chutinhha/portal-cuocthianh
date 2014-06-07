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
    public interface IShoppingCartActionService
    {
        long Add(ShoppingCart _model);
        long RemoveAt(long id);
        int Update(Product _product, int qty);
        List<CoreData.ShoppingCart> GetList();
        double GetTotalPrice();
        int GetAmount();
        int RemoveAllCart();
        int RemoveSelectCart(int[] arrSelectID);
    }
    public partial class ShoppingCartActionService : IShoppingCartActionService
    {
        ShoppingCartService Service;

        public ShoppingCartActionService(ShoppingCartService _Service)
       {
           Service = _Service;
       }

        public virtual long Add(ShoppingCart _model){
            return Service.Add(_model);
        }

        public virtual long RemoveAt(long id)
        {
            return Service.RemoveAt(id);
        }

        public virtual int Update(Product _product, int qty)
        {
            return Service.Update(_product, qty);
        }

        public virtual List<CoreData.ShoppingCart> GetList()
        {
            return Service.GetList();
        }

        public virtual double GetTotalPrice()
        {
            return Service.GetTotalPrice();
        }

        public virtual int GetAmount()
        {
            return Service.GetAmount();
        }

        public virtual int RemoveAllCart()
        {
            return Service.RemoveAllCart();
        }

        public virtual int RemoveSelectCart(int[] arrSelectID)
        {
            return Service.RemoveSelectCart(arrSelectID);
        }
    }
}
