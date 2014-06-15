using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public class ValueDefine
    {
        public enum BannerLogoType
        {
            Banner = 0,
            Logo = 1
        }
        public enum BannerLogoPosition {
            //Main Slider tren trang chinh
            ShideShow=0,
            //Banner ben phai Slider chinh
            RightSlider=1,
            //Banner ben cot trai
            LeftBanner=2,
            //Banner ben cot phai
            RightBanner=3,
            //List Banner duoi Footer
            FooterBanner=4
        }
        public enum OrderStatus
        {
            New = 1,
            Pending = 2,
            Completed = 3,
            Paid = 4,
            Cancel =5
        }
        public enum CustomerIdea
        {
            Order = 1,
            Contact = 2,
            Article = 3
        }
        public enum Paymethod
        {
            Paypal = 1,
            CreaditCard = 2,
            Bank = 3,
            DirectAtStore = 4,
            PaidWhenReceiveOrder = 5
        }
        public enum ShipMethod
        {
            QuaShopNhanHang = 1,
            GiaoHangTanNoi = 2,
        }

        public enum Manager
        {
            Customer = 1,
            Manager = 2,

        }

        public enum ArticleCategory
        {
            HoTro = 1,
            HuongDan = 2,
            Khac = 3,
        }

        public enum SearchPrice
        {
            duoi1trieu=1,
            duoi2trieu=2,
            duoi3trieu=3,
            duoi5trieu=4,
            duoi8trieu=5,
            duoi10trieu=6,
            tren10trieu=7
        }

        public const string MANUFACTURER = "MANUFACTURER";
        public const string CATEGORY = "CATEGORY";
        public const string PRODUCT = "PRODUCT";
        public const string USER = "USER";
        public const string ORDER = "ORDER";
        public const string ARTICLE = "ARTICLE";
        public const string PRODUCTATTRIBUTE = "PRODUCTATTRIBUTE";
        public const string PROVINCE = "PROVINCE";
        public const string DICSTRICT = "DICSTRICT";
        public const string ATTRIBUTE = "ATTRIBUTE";
        public const string CONFIG = "CONFIG";
        public const string PERMISSION = "PERMISSION";
        public const string CUSTOMERIDEA = "CUSTOMERIDEA";
        public const string BANNERLOGO = "BANNERLOGO";
        public const string CATEGOPRYARTICLE = "CATEGOPRYARTICLE";
    }
}
