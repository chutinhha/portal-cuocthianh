using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreData;
using EntityData;
using Helper;
using System.Web;
using System.Web.Mvc;

namespace Services
{
    public partial class ShoppingCartService
    {
        // <summary>
        /// Danh sách sản phẩm và số lượng tương ứng trong giỏ hàng
        /// </summary>
        List<ShoppingCart> ListCartItem = new List<ShoppingCart>();

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="item"></param>
        public long Add(ShoppingCart item)
        {
            try
            {
                //hàm viết bậy
                foreach (var i in ListCartItem)
                {
                    if (i.Product.ID == item.Product.ID)
                    {
                        item.Qty += i.Qty;
                    }
                }
                //ListCartItem.RemoveAll(x => x.Product.ID == item.Product.ID);
                ListCartItem.Add(item);
                return 1;
            }
            catch {
                return -1;
            }
        }
        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        /// <param name="item"></param>
        public long RemoveAt(long Id)
        {
            try
            {
                ListCartItem.RemoveAll(x => x.Product.ID == Id);
                return Id;
            }
            catch {
                return -1;
            }
        }
        /// <summary>
        /// Cập nhật sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="Id"></param>
        public int Update(Product product, int qty)
        {
            //hàm cần viết lại
            try
            {
                ListCartItem.RemoveAll(x => x.Product.ID == product.ID);
                ListCartItem.Add(new ShoppingCart() { Product = product, Qty = qty });
                return 1;
            }
            catch {
                return -1;
            }
        }
        /// <summary>
        /// Lấy danh sách sản phẩm và số lượng tương ứng trong giỏ hàng
        /// </summary>
        /// <returns></returns>
        public List<CoreData.ShoppingCart> GetList()
        {
            return ListCartItem;
        }

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (var item in ListCartItem)
            {
                total += (double)(item.Qty * item.Product.Price);
            }
            return total;
        }

        public int GetAmount()
        {
            int Qty = 0;
            foreach (var item in ListCartItem)
            {
                Qty += item.Qty;
            }
            return Qty;
        }

        public int RemoveAllCart()
        {
            try
            {
                ListCartItem.Clear();
                return 1;
            }
            catch {
                return -1;
            }
        }

        public int RemoveSelectCart(int[] arrSelectID)
        {
            try
            {
                foreach (var item in arrSelectID)
                {
                    RemoveAt(item);
                }
                return 1;
            }
            catch {
                return -1;
            }
        }
    }
}
