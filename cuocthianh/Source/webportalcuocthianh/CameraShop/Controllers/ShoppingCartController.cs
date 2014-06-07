using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using Helper;
using ActionServices;

namespace CameraShop.Controllers
{
    public class ShoppingCartController : BaseController
    {
        //
        // GET: /ShopCart/

        public ShoppingCartController(IProductActionService _product, ShoppingCartActionService _shoppingcart)
            : base(_product, _shoppingcart)
        { }

        static List<ShoppingCart> ShopCart = new List<ShoppingCart>();
        /// <summary>
        /// Trang Index của ShoppingCart
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(ShopCart);
        }
        /// <summary>
        /// Partial View ShoppingCart
        /// </summary>
        /// <returns></returns>
        public ActionResult _ListCart() {
            return PartialView(ShopCart); 
        }
        /// <summary>
        /// Add Cart
        /// </summary>
        /// <param name="ProductId">Product ID</param>
        /// <param name="Qty">Cart Amount</param>
        /// <returns></returns>
        public JsonResult AddToCart(long ProductId, int Qty=1)
        {
            var product = ProductService.GetByID(ProductId);
            if (ShopCart == null)
            {
                ShopCart.Clear();
            }
            if (Session["ShoppingCart"] != null)
            {
                ShopCart = (List<ShoppingCart>)Session["ShoppingCart"];
                var itemcart = ShopCart.Where(c => c.Product.ID == ProductId).FirstOrDefault();
                if (itemcart != null)
                {
                    itemcart.Amount++;
                    itemcart.TotalPrice += itemcart.Price * itemcart.Amount;
                }
                else
                {
                    ShoppingCart _itemcart = new ShoppingCart();

                    _itemcart.Price = product.Price;
                    _itemcart.Amount = Qty;
                    _itemcart.TotalPrice = _itemcart.Amount * _itemcart.Price;

                    _itemcart.Product = new Product();
                    _itemcart.Product = product;
                    ShopCart.Add(_itemcart);
                }
                Session["ShoppingCart"] = ShopCart;
            }
            else {
                ShopCart = new List<ShoppingCart>();
                ShoppingCart _itemcart = new ShoppingCart();

                _itemcart.Price = product.Price;
                _itemcart.Amount = Qty;
                _itemcart.TotalPrice = _itemcart.Amount * _itemcart.Price;

                _itemcart.Product = new Product();
                _itemcart.Product = product;
                ShopCart.Add(_itemcart);
                Session["ShoppingCart"] = ShopCart;
            }
            return Json(ShopCart);
        }
        /// <summary>
        /// Show cart on Header
        /// </summary>
        /// <returns></returns>
        public ActionResult _CartHeader() {
            return PartialView(ShopCart);
        }
        public ActionResult UpdateCart(string[] p_qty)
        {
            List<ShoppingCart> model = new List<ShoppingCart>();
            model = (List<ShoppingCart>)Session["ShoppingCart"];
            foreach (var item in p_qty)
            {
                string[] arr = item.Split(',');
                int productId = int.Parse(arr[0]);
                int qty = int.Parse(arr[1]);
                var _product = ProductService.GetByID(productId);
                ShoppingCartService.Update(_product, qty);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
        /// <summary>
        /// Remove Cart Item by Product ID
        /// </summary>
        /// <param name="productid">Product ID</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int productid)
        {
            ShopCart = (List<ShoppingCart>)Session["ShoppingCart"];
            ShopCart.RemoveAll(x => x.Product.ID == productid);
            Session["ShoppingCart"] = ShopCart;
            return RedirectToAction("Index", "ShoppingCart");
        }
        /// <summary>
        /// Remove Selected Cart
        /// </summary>
        /// <param name="arrSelect">Array Select Cart Id</param>
        /// <returns></returns>
        public ActionResult RemoveSelectCart(int[] arrSelect)
        {
            ShopCart = (List<ShoppingCart>)Session["ShoppingCart"];
            try
            {
                foreach (var item in arrSelect)
                {
                    ShopCart.RemoveAll(x => x.Product.ID == item);
                }
                Session["ShoppingCart"] = ShopCart;
                return PartialView("_ListCart");
            }
            catch
            {
                return PartialView("_ListCart");
            }
        }
        /// <summary>
        /// Add Cart to Shopping Cart and Redirect to Payment
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Qty"></param>
        /// <returns></returns>
        public ActionResult BuyNow(int ProductId, int Qty=1)
        {
            //ShopCart = (List<ShoppingCart>)Session["ShoppingCart"];
            if (ShopCart == null)
            {
                ShopCart.Clear();
            }
            var product = ProductService.GetByID(ProductId);
            var itemcart = ShopCart.Where(c => c.Product.ID == ProductId).FirstOrDefault();
            if (itemcart != null)
            {
                itemcart.Amount += Qty;
                itemcart.TotalPrice += itemcart.Price * itemcart.Amount;
            }
            else
            {
                ShoppingCart _itemcart = new ShoppingCart();

                _itemcart.Price = product.Price;
                _itemcart.Amount = Qty;
                _itemcart.TotalPrice = _itemcart.Amount * _itemcart.Price;

                _itemcart.Product = new Product();
                _itemcart.Product.ID = ProductId;
                _itemcart.Product.Price = product.Price;
                _itemcart.Product.Name = product.Name;
                _itemcart.Product.Image = product.Image;
                _itemcart.Product.ProductAttributeExt = product.ProductAttributeExt;

                ShopCart.Add(_itemcart);
            }
            Session["ShoppingCart"] = ShopCart;
            return RedirectToAction("Index","PayMent");
        }

    }
}
