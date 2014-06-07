using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreData;
using ActionServices;
using Helper;
using PayPalAPIHelper;
//using CameraShop.Models;
using System.Configuration;
namespace CameraShop.Controllers
{
    public class PaymentController:BaseController
    {
        public PaymentController(IUserActionService _user, IOrderActionService _order,
            IOrderDetailActionService _orderdetail,
            IProductActionService _product, IEmailListActionService _emaillist,
            IEmailTemplateActionService _mailtemplate, IShoppingCartActionService _shoppingcart)
            : base(_user, _order, _orderdetail, _product, _emaillist, _mailtemplate, _shoppingcart)
        { }
        // GET: /Payment/

        public ActionResult Index()
        {
            List<ShoppingCart> model = (List<ShoppingCart>)Session["ShoppingCart"];

            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }




        void ImportMail(int template,int orderid, string customername, string customeradd, string customerphone, string link,int total)
        {
            try
            {
                var email = EmailTemplateService.GetByID(2);
                var useremail = UserService.GetByID(Userid);
                if (useremail != null)
                {
                    var lstnail = new EmailList();
                    lstnail.Email = useremail.Email;
                    lstnail.TitleExt = "Camera shop - Thông tin đơn hàng"; //String.Format("{0:0,0 vnđ}", price)
                    link = Helper.StringHelper.GetHost() + "/" + "order/detail/" + orderid;
                    lstnail.BodyExt = email.Template.Replace("{customername}", customername).Replace("{customeraddress}", customeradd).Replace("{link}", link).Replace("{customerphone}",customerphone);
                    string detail = Helper.EmailHelper.TableHeader() + GetDetailOrderMail() + Helper.EmailHelper.TableFooter(total);
                    lstnail.BodyExt = lstnail.BodyExt.Replace("{details}", detail);
                    MvcApplication.EmailQueue.Add(lstnail);
                }
            }
            catch { }
        }

         string GetDetailOrderMail()
        {
            string detail = "";// Helper.EmailHelper.TableHeader();
            ShoppingCart s = (ShoppingCart)Session["ShoppingCart"];
            var cartitem = ShoppingCartService.GetList();
            foreach (var item in cartitem)
            {
                detail += Helper.EmailHelper.TableOrderDetail(item.Product.Name, item.Qty, "10%", item.Product.Price);
               
              
            }
            return detail;
        }


        #region Pay

        public ActionResult PayWithCreditCard()
        {
            if (OrderID ==0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult PayCreditCard(CreditCard model)
        {
            ShoppingCart s = (ShoppingCart)Session["ShoppingCart"];
            PayPalCreditCardHelper paypal = new PayPalCreditCardHelper();
            double TotalPrice = (ShoppingCartService.GetTotalPrice());
            int quantity = (ShoppingCartService.GetList().Count);
            TotalPrice = Helper.StringHelper.VNDTOUSD(TotalPrice, Helper.RateCurrencyHelper.GetUSDRate());
            var result = paypal.Pay(OrderID.ToString(), TotalPrice.ToString("0.##").Replace(",", "."), model.Lastname, model.Firstname, model.Address, model.City, model.State, model.CountryCode, model.CountryName, model.Zipcode, model.Cardtype, model.CardNumber, model.CVV2, model.ExpriredMonth, model.ExpriredYear);
            if (result.IsSucess == true)
            {
                var host = "http://" + Request.Url.Host + ":" + Request.Url.Port;
                var order = OrderService.GetByID(OrderID);
                order.Status = (int)Helper.ValueDefine.OrderStatus.Paid;
                order.Note = "Thanh toán qua thẻ Credit card";
                OrderService.Save(order);
                UpdateProductSole();
                ImportMail(2, OrderID, order.Name, order.Address, order.Phone, "", order.TotalPrice);
                OrderID = 0;
                return Content(host + "/Payment/PayOnlineDone?type=CreditCard");
            }
            else
            {
                return Content("Error");
            }
        }


        public ActionResult PayAtStore()
        {
             if(OrderID ==0)
            {
                return RedirectToAction("Index", "Home");
            }
             var order = OrderService.GetByID(OrderID);
             order.Status = (int)Helper.ValueDefine.OrderStatus.New;
             order.Note = "Khách hàng sẽ đến trực tiếp cửa hàng để thanh toán";
             OrderService.Save(order);
             ImportMail(2, OrderID, order.Name, order.Address, order.Phone, "", order.TotalPrice);
             ClearCart();
            OrderID = 0;
            return View();
        }

        public ActionResult PayByBank()
        {
             if(OrderID ==0)     
            {
                return RedirectToAction("Index", "Home");
            }
             var order = OrderService.GetByID(OrderID);
             order.Status = (int)Helper.ValueDefine.OrderStatus.New;
             order.Note = "Khách hàng sẽ thanh toán qua ngân hàng (chuyển khoản)";
             OrderService.Save(order);
             ImportMail(2, OrderID, order.Name, order.Address, order.Phone, "", order.TotalPrice);
             ClearCart();
            OrderID = 0;
            return View();
        }


        public ActionResult PaypalStandardConfirm()
        {
            if (Session["PayPalToken"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var payPalToken = Session["PayPalToken"] as string;
            var  isTest = (bool)Session["IsText"];
             
            string amount;
            string currecny;
            string itemName;
            string itemNumber;
            string orderDescription;
            string quantity;
            string payerDetails;
            if (isTest)
            {
                PayPalExpressHelper.GetExpressCheckoutDetails(PayPalEnvironment.SandBox, payPalToken, out amount, out currecny, out itemName, out itemNumber, out orderDescription, out quantity, out payerDetails);
            }
            else
            {
                PayPalExpressHelper.GetExpressCheckoutDetails(PayPalEnvironment.PayPal, payPalToken, out amount, out currecny, out itemName, out itemNumber, out orderDescription, out quantity, out payerDetails);
            }

            string data = amount + "_" + currecny + "_" + itemName + "_" + itemNumber + "_" + orderDescription + "_" + quantity;
            data += "_" + Request["PayerID"];
          
            return View("PaypalStandardConfirm",(object)data);
        }

        //public ActionResult PayOnlineDone(string PayerID, string Amount, string Currentcy, string type)
        //{

        //    //if (Session["PayPalToken"] == null)
        //    //{
        //    //    return RedirectToAction("Index", "Home");
        //    //}
        //    var order = OrderService.GetByID(OrderID);
        //    if (type == "Paypal")
        //    {
        //        var isTest = (bool)Session["IsText"];
        //        var payPalToken = Session["PayPalToken"] as string;
        //        bool isOK = PayPalExpressHelper.DoExpressCheckoutPayment(PayPalEnvironment.SandBox, payPalToken, PayerID, Amount, (PayPalCurrency)(Enum.Parse(typeof(PayPalCurrency), Currentcy)));

        //        if (isOK)
        //        {
        //            Session["PayPalToken"] = null;
        //           //var order = OrderService.GetByID(OrderID);
        //           order.Status = (int)Helper.ValueDefine.OrderStatus.Paid;
        //           order.Note = "Khách hàng đã thanh toán qua paypal";
        //           OrderService.Save(order);
        //           UpdateProductSole();
        //           ImportMail(2, OrderID, order.Name, order.Address, order.Phone, "", order.TotalPrice);
        //           ClearCart();
        //            return View();
        //        }
        //        else
        //        {
        //            return View("Error");
        //        }
        //    }
        //    try
        //    {
        //        UpdateProductSole();
        //    }
        //    catch { }
          
        //    ClearCart();
        //     return View();
            
        //}

        //public ActionResult PaymentAction(int paymethod, int waypay, string address, string name, string phone)
        //{
        //    var host = "http://"+Request.Url.Host+":"+Request.Url.Port;
        //    ShoppingCart s = (ShoppingCart)Session["ShoppingCart"];
        //    Order _order = new Order();
        //    _order.Active = true;
        //    _order.Address = address;
        //    _order.Name = name;
        //    _order.Note = "Đơn hàng mới, chưa được thanh toán";
        //    _order.Phone = phone;
        //    _order.CreateDate = DateTime.Now;
        //    _order.Ship_Fee = 0;
        //    _order.ShipMethod = 2;
           
        //    _order.Status = (int)Helper.ValueDefine.OrderStatus.New;
        //    _order.TotalPrice = int.Parse(ShoppingCartService.GetTotalPrice().ToString());
        //    _order.UserID = Userid;
        //    var id = OrderService.Save(_order);
        //    OrderID = int.Parse(id.ToString());
        //    if (id > 0)
        //    {
        //        var cartitem = ShoppingCartService.GetList();
        //        foreach (var item in cartitem)
        //        {
        //            var _item = new OrderDetail();
        //            _item.OrderID = id;
        //            _item.Price = item.Product.Price ;
        //            _item.ProductID = item.Product.ID;
        //            _item.Total = item.Product.Price * item.Qty;
        //            _item.Amount = item.Qty;
        //            OrderDetailService.Save(_item);
        //        }
        //    }
        //    if (paymethod == 1)
        //    {
        //        _order.PaymentMethod = (int)Helper.ValueDefine.Paymethod.Paypal;
        //        _order.ID = id;
        //        OrderService.Save(_order);
        //        if (waypay == 0)
        //        {
        //            string payPalToken;
        //            PayPalCurrency currency = (PayPalCurrency)Enum.Parse(typeof(PayPalCurrency), "USD");
        //          //  ShoppingCart s = (ShoppingCart)Session["ShoppingCart"];

        //            double TotalPrice = (ShoppingCartService.GetTotalPrice());
        //            int quantity = (ShoppingCartService.GetList().Count);
        //            TotalPrice = Helper.StringHelper.VNDTOUSD(TotalPrice, Helper.RateCurrencyHelper.GetUSDRate());
        //            string returnUrl = PayPalExpressHelper.CreatePaypalExpressPayment(PayPalEnvironment.SandBox, "Đơn hàng_"+id, TotalPrice, currency, quantity, "Đơn hàng", null, ConfigurationManager.AppSettings["PaypalReturnUrl"], ConfigurationManager.AppSettings["PaypalCancelUrl"], out payPalToken);

        //            Session["PayPalToken"] = payPalToken;
        //            Session["IsText"] = true;

        //            return Content(returnUrl);
        //        }
        //        else
        //        {
        //            _order.PaymentMethod = (int)Helper.ValueDefine.Paymethod.CreaditCard;
        //            _order.ID = OrderID;
        //            OrderService.Save(_order);
        //            return Content(host+"/Payment/PayWithCreditCard");
        //        }
        //    }
        //    if (paymethod == 3)
        //    {
        //        _order.PaymentMethod = (int)Helper.ValueDefine.Paymethod.Bank;
        //        _order.ID = OrderID;
        //        OrderService.Save(_order);
        //        return Content(host+"/Payment/PayByBank");// RedirectToAction("PayByBank", "Payment");
        //    }
        //    if (paymethod == 4)
        //    {
        //        _order.PaymentMethod = (int)Helper.ValueDefine.Paymethod.DirectAtStore;
        //        _order.ID = OrderID;
        //        OrderService.Save(_order);
        //        return Content(host+"/Payment/PayAtStore"); //RedirectToAction("PayAtStore", "Payment");
        //    }
        //    return View();
        //}

        public ActionResult PayDone(string PayerID, string Amount, string Currentcy, string type)
        {
            var host = "http://" + Request.Url.Host + ":" + Request.Url.Port;
            var order = OrderService.GetByID(OrderID);
            //var order = OrderService.GetByID(OrderID);
            order.Status = (int)Helper.ValueDefine.OrderStatus.Paid;
            order.Note = "Khách hàng đã thanh toán qua paypal";
            OrderService.Save(order);
            //UpdateProductSole();
            ImportMail(2, OrderID, order.Name, order.Address, order.Phone, "", order.TotalPrice);
            ClearCart();
            return View();
        }
        public ActionResult PaymentAction(int waypay, string address, string name, string phone)
        {
            List<ShoppingCart> model = (List<ShoppingCart>)Session["ShoppingCart"];
            var host = "http://" + Request.Url.Host + ":" + Request.Url.Port;
            Order _order = new Order();
            _order.Active = true;
            _order.Address = address;
            _order.Name = name;
            _order.Note = "Đơn hàng mới, chưa được thanh toán";
            _order.Phone = phone;
            _order.CreateDate = DateTime.Now;
            _order.Ship_Fee = 0;
            _order.ShipMethod = 2;

            _order.Status = (int)Helper.ValueDefine.OrderStatus.New;
            _order.TotalPrice = int.Parse(ShoppingCartService.GetTotalPrice().ToString());
            _order.UserID = Userid;
            var id = OrderService.Save(_order);
            OrderID = int.Parse(id.ToString());
            if (id > 0)
            {
                var cartitem = model;
                foreach (var item in cartitem)
                {
                    var _item = new OrderDetail();
                    _item.OrderID = id;
                    _item.Price = item.Product.Price;
                    _item.ProductID = item.Product.ID;
                    _item.Total = item.Product.Price * item.Qty;
                    _item.Amount = item.Amount;
                    OrderDetailService.Save(_item);
                }
            }
            _order.ID = id;
            OrderService.Save(_order);
            return Content(host + "/Payment/PayDone");
        }
        #endregion


        #region Other

        [HttpPost]
        public ActionResult LoginPayment(User _user)
        {
          
            if(String.IsNullOrEmpty(_user.UserName)||String.IsNullOrEmpty(_user.Password))
                return Content("Vui lòng nhập đầy đủ thông tin");
            if (UserService.Login(_user, ref Username, ref Userid, ref Groupid) == true)
            {
                Session["UserName"] = Username;
                Session["UserID"] = Userid;
                Session["UserGroup"] = Groupid;

                return Content("ok");
            }
            else
            {
                Session["UserName"] = null;
                Session["UserGroup"] = null;
                Session["UserID"] = null;
                return Content("Tài khoản hoặc mật khẩu không đúng");
            }
        }


        [HttpPost]
        public ActionResult GetInfo()
        {
            try {
                var data = UserService.GetByID(Userid);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch { return Content(""); }
        }


        void ClearCart()
        {
            List<ShoppingCart> model = (List<ShoppingCart>)Session["ShoppingCart"];
            model.Clear();
            Session["ShoppingCart"] = model;
        }

        void UpdateProductSole()
        {
            List<ShoppingCart> model = (List<ShoppingCart>)Session["ShoppingCart"];
            foreach (var item in model)
            {
                ProductService.UpdateSoleAmount(int.Parse(item.Product.ID.ToString()), item.Amount);
            }
        }

        #endregion


    }
}
