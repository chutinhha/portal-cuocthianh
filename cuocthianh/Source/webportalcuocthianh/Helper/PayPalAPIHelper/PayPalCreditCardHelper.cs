using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPalAPIHelper.PayPalWS;
using System.Configuration;

namespace PayPalAPIHelper
{
    public class PayPalCreditCardHelper
    {
        public PayPalReturn Pay(string orderNumber, string paymentAmount, string buyerLastName, string buyerFirstName, string buyerAddress, string buyerCity, string buyerStateOrProvince, string buyerCountryCode, string buyerCountryName, string buyerZipCode, string creditCardType, string creditCardNumber, string CVV2, string expMonth, string expYear)
        {
            //PayPal Return Structure
            PayPalReturn rv = new PayPalReturn();
            rv.IsSucess = false;
     
            DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
            requestDetails.CreditCard = new CreditCardDetailsType();
            requestDetails.CreditCard.CardOwner = new PayerInfoType();
            requestDetails.CreditCard.CardOwner.Address = new AddressType();
            requestDetails.PaymentDetails = new PaymentDetailsType();
            requestDetails.PaymentDetails.OrderTotal = new BasicAmountType();
            requestDetails.CreditCard.CardOwner.PayerName = new PersonNameType();

            //Request
            requestDetails.PaymentAction = PaymentActionCodeType.Sale;
            requestDetails.IPAddress = "127.0.0.1"; //HttpContext.Current.Request.UserHostAddress;

            //Payment
            requestDetails.PaymentDetails.OrderTotal.currencyID = CurrencyCodeType.USD;
            requestDetails.PaymentDetails.OrderTotal.Value = paymentAmount;
            requestDetails.MerchantSessionId = "Z46G3JU4SEEDC";

            //requestDetails.PaymentDetails.AllowedPaymentMethod = AllowedPaymentMethodType.Default;

            //  requestDetails.PaymentDetails.PaymentAction = PaymentActionCodeType.Sale;

            //Credit card
            requestDetails.CreditCard.CreditCardNumber = creditCardNumber;
            requestDetails.CreditCard.CreditCardType = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), creditCardType, true);
            requestDetails.CreditCard.ExpMonth = Convert.ToInt32(expMonth);
            requestDetails.CreditCard.ExpYear = Convert.ToInt32(expYear);
            requestDetails.CreditCard.CVV2 = CVV2;
            requestDetails.CreditCard.CreditCardTypeSpecified = true;
            requestDetails.CreditCard.ExpMonthSpecified = true;
            requestDetails.CreditCard.ExpYearSpecified = true;

            //Card Owner
            requestDetails.CreditCard.CardOwner.PayerName.FirstName = buyerFirstName;
            requestDetails.CreditCard.CardOwner.PayerName.LastName = buyerLastName;
            requestDetails.CreditCard.CardOwner.Address.Street1 = buyerAddress;
            requestDetails.CreditCard.CardOwner.Address.CityName = buyerCity;
            requestDetails.CreditCard.CardOwner.Address.StateOrProvince = buyerStateOrProvince;
            requestDetails.CreditCard.CardOwner.Address.CountryName = buyerCountryName;
            requestDetails.CreditCard.CardOwner.Address.Country = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), buyerCountryCode, true);
            requestDetails.CreditCard.CardOwner.Address.PostalCode = buyerZipCode;
            requestDetails.CreditCard.CardOwner.Address.CountrySpecified = true;
            requestDetails.CreditCard.CardOwner.PayerCountry = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), buyerCountryCode, true);
            requestDetails.CreditCard.CardOwner.PayerCountrySpecified = true;

            DoDirectPaymentReq request = new DoDirectPaymentReq();
            request.DoDirectPaymentRequest = new DoDirectPaymentRequestType();
            request.DoDirectPaymentRequest.DoDirectPaymentRequestDetails = requestDetails;
            request.DoDirectPaymentRequest.Version = "63";

            //Headers
            CustomSecurityHeaderType headers = new CustomSecurityHeaderType();

            headers.Credentials = new UserIdPasswordType();
            //"linhdaigia_crista_api1.yahoo.com.vn"; 
            //"KAM8YZ2ZG6P9GRM5";
            //"A.FSKSXlaJhdHcmij1IXGU62TZ6vAuB5Sq1ZbiMYCO-vRFuWXiSJSfn5";
            //headers.Credentials.Username = "linhdaigia_crista_api1.yahoo.com.vn";
            //headers.Credentials.Password = "KAM8YZ2ZG6P9GRM5";
            //headers.Credentials.Signature = "A.FSKSXlaJhdHcmij1IXGU62TZ6vAuB5Sq1ZbiMYCO-vRFuWXiSJSfn5";
            //headers.Credentials.Subject = ""; //"duylin_1362149130_biz@gmail.com";

            headers.Credentials.Username = ConfigurationManager.AppSettings["PayPalSandboxAPIUserName"];
            headers.Credentials.Password = ConfigurationManager.AppSettings["PayPalSandboxAPIPassword"];
            headers.Credentials.Signature = ConfigurationManager.AppSettings["PayPalSandboxAPISignature"];
            headers.Credentials.Subject = ""; //"duylin_1362149130_biz@gmail.com";

            //   headers.DidUnderstand = true;


            //Client  

            PayPalAPIAASoapBinding client = new PayPalAPIAASoapBinding();
            //  client.Url = "https://api-3t.sandbox.paypal.com/2.0/";
            client.Url = "https://api-3t.sandbox.paypal.com/nvp";
            //    client.Url="https://api-3t.paypal.com/2.0/";
            //  client.Url = "https://api-aa-3t.paypal.com/2.0/";
            // client.Url = "https://api-aa.sandbox.paypal.com/2.0/";
            client.RequesterCredentials = headers;
            client.Timeout = 15000;
            // client.UseDefaultCredentials = true;

            //   ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;


            // allows for validation of SSL conversations
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            DoDirectPaymentResponseType response = client.DoDirectPayment(request);
            if (response.Ack == AckCodeType.Success || response.Ack == AckCodeType.SuccessWithWarning)
            {
                rv.IsSucess = true;
                rv.TransactionID = response.TransactionID;
            }
            else
            {
                rv.ErrorMessage = response.Errors[0].LongMessage;
                rv.errorcode = response.Errors[0].ErrorCode;
            }
            return rv;
        }
    }
}
