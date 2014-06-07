using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Helper
{
    public class RateCurrencyHelper
    {
        ////get usb from vietcombank
        public static int  GetUSDRate()
        {
            var config = ExternalDatabaseHelper.ExtendModel.ExtendDBModel.Configurations.Where(c => c.Code.Equals("Rateusd")).First();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                String xmlSourceUrl = "http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx";
                xmlDocument.Load(xmlSourceUrl);
                XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Exrate");
                var data= nodeList.Item(nodeList.Count - 1).Attributes["Transfer"].InnerText;
                if(!String.IsNullOrEmpty(data))
                {
                   
                    config.Value = data;
                    ExternalDatabaseHelper.ExtendModel.ExtendDBModel.SaveChanges();
                  //  ex.
                }
                return int.Parse(config.Value);
               
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(config.Value))
                    return int.Parse(config.Value);
                return 0;
            }
        }
    }
}
