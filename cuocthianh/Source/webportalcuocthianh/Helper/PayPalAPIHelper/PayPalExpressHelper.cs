using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
namespace PayPalAPIHelper
{
    public static class PayPalExpressHelper
    {
        #region CreatePaypalExpressPayment
        public static string CreatePaypalExpressPayment(PayPalEnvironment Environment, string orderDescription, double itemPrice, PayPalCurrency currency, int quantity, string ItemName, string ItemNumber, string ReturnUrl, string CancelUrl,  out string paypalToken)
        {
            if (Environment == PayPalEnvironment.SandBox)
            {
                return CreatePaypalExpressPaymentSandbox(orderDescription, itemPrice, currency, quantity, ItemName, ItemNumber, ReturnUrl, CancelUrl, out paypalToken);
            }
            else
            {
                return CreatePaypalExpressPaymentReal(orderDescription, itemPrice, currency, quantity, ItemName, ItemNumber, ReturnUrl, CancelUrl, out paypalToken);
            }
        }

        private static string CreatePaypalExpressPaymentSandbox(string orderDescription, double itemPrice, PayPalCurrency currency, int quantity, string ItemName, string ItemNumber, string ReturnUrl, string CancelUrl, out string paypalToken)
        {
          
            double totalPrice = itemPrice;// itemPrice* quantity;
            // build request
            PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutRequestDetailsType reqDetails = new PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutRequestDetailsType();

            reqDetails.ReturnURL = ReturnUrl;
            reqDetails.CancelURL = CancelUrl;
            reqDetails.NoShipping = "1";
            reqDetails.OrderDescription = orderDescription;
            reqDetails.OrderTotal = new PayPalAPIHelper.PayPalSandboxWS.BasicAmountType()
            {
                currencyID = ConvertProgramCurrencyToPayPalSandbox(currency),
                Value = totalPrice.ToString("0.##").Replace(",", ".")
            };

            PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsType paymentDetails = new PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsType();

            PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsItemType item = new PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsItemType();
            item.Amount = new PayPalAPIHelper.PayPalSandboxWS.BasicAmountType();
            item.Amount.currencyID = ConvertProgramCurrencyToPayPalSandbox(currency);
            item.Amount.Value = totalPrice.ToString("0.##").Replace(",", ".");
            item.Name = ItemName;
             item.Number = quantity.ToString();
            item.Quantity = "1"; //quantity.ToString();

            paymentDetails.PaymentDetailsItem = new PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsItemType[] { item };

            reqDetails.PaymentDetails = new PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsType[] { paymentDetails };


            PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutReq req = new PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutReq()
            {
                SetExpressCheckoutRequest = new PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutRequestType()
                {
                    Version = Version,
                    SetExpressCheckoutRequestDetails = reqDetails
                }
            };

            // query PayPal and get token
            PayPalAPIHelper.PayPalSandboxWS.SetExpressCheckoutResponseType resp = BuildPayPalSandboxWebservice().SetExpressCheckout(req);
            HandleErrorSandbox(resp);
            paypalToken = resp.Token;

            // return a URL to PayPal
            return (string.Format("{0}?cmd=_express-checkout&token={1}", "https://www.sandbox.paypal.com/cgi-bin/webscr", paypalToken));
        }

        private static string CreatePaypalExpressPaymentReal(string orderDescription, double itemPrice, PayPalCurrency currency, int quantity, string ItemName, string ItemNumber, string ReturnUrl, string CancelUrl, out string paypalToken)
        {
            double totalPrice = itemPrice;// itemPrice* quantity;

            // build request
            PayPalAPIHelper.PayPalWS.SetExpressCheckoutRequestDetailsType reqDetails = new PayPalAPIHelper.PayPalWS.SetExpressCheckoutRequestDetailsType();

            reqDetails.ReturnURL = ReturnUrl;
            reqDetails.CancelURL = CancelUrl;
            reqDetails.NoShipping = "1";
            reqDetails.OrderDescription = orderDescription;
            reqDetails.OrderTotal = new PayPalAPIHelper.PayPalWS.BasicAmountType()
            {
                currencyID = ConvertProgramCurrencyToPayPal(currency),
                Value = totalPrice.ToString("0:0.##").Replace(",",".")
            };

            PayPalAPIHelper.PayPalWS.PaymentDetailsType paymentDetails = new PayPalAPIHelper.PayPalWS.PaymentDetailsType();

            PayPalAPIHelper.PayPalWS.PaymentDetailsItemType item = new PayPalAPIHelper.PayPalWS.PaymentDetailsItemType();
            item.Amount = new PayPalAPIHelper.PayPalWS.BasicAmountType();
            item.Amount.currencyID = ConvertProgramCurrencyToPayPal(currency);
            item.Amount.Value = totalPrice.ToString("0.##").Replace(",", ".");
            item.Name = ItemName;
            item.Number = quantity.ToString();
            item.Quantity = "Số đơn hàng: 1";

            paymentDetails.PaymentDetailsItem = new PayPalAPIHelper.PayPalWS.PaymentDetailsItemType[] { item };

            reqDetails.PaymentDetails = new PayPalAPIHelper.PayPalWS.PaymentDetailsType[] { paymentDetails };

            PayPalAPIHelper.PayPalWS.SetExpressCheckoutReq req = new PayPalAPIHelper.PayPalWS.SetExpressCheckoutReq()
            {
                SetExpressCheckoutRequest = new PayPalAPIHelper.PayPalWS.SetExpressCheckoutRequestType()
                {
                    Version = Version,
                    SetExpressCheckoutRequestDetails = reqDetails
                }
            };

            // query PayPal and get token
            PayPalAPIHelper.PayPalWS.SetExpressCheckoutResponseType resp = BuildPayPalWebservice().SetExpressCheckout(req);
            HandleError(resp);
          
            paypalToken = resp.Token;
           
            // return a URL to PayPal
            return (string.Format("{0}?cmd=_express-checkout&token={1}", "https://www.paypal.com/cgi-bin/webscr", paypalToken));
        }
        #endregion

        #region GetExpressCheckoutDetails
        public static void GetExpressCheckoutDetails(PayPalEnvironment Environment, string token, out string amount, out string currecny, out string itemName, out string itemNumber, out string orderDescription, out string quantity, out string payerDetails)
        {
            if (Environment == PayPalEnvironment.SandBox)
            {
                GetExpressCheckoutDetailsSandBox(token, out amount, out currecny, out itemName, out itemNumber, out orderDescription, out quantity, out payerDetails);
            }
            else
            {
                GetExpressCheckoutDetailsReal(token, out amount, out currecny, out itemName, out itemNumber, out orderDescription, out quantity, out payerDetails);
            }
        }


        private static void GetExpressCheckoutDetailsReal(string token, out string amount, out string currecny, out string itemName, out string itemNumber, out string orderDescription, out string quantity, out string payerDetails)
        {
            // build getdetails request
            PayPalAPIHelper.PayPalWS.GetExpressCheckoutDetailsReq req = new PayPalAPIHelper.PayPalWS.GetExpressCheckoutDetailsReq()
            {
                GetExpressCheckoutDetailsRequest = new PayPalAPIHelper.PayPalWS.GetExpressCheckoutDetailsRequestType()
                {
                    Version = Version,
                    Token = token
                }
            };

            // query PayPal for transaction details
            PayPalAPIHelper.PayPalWS.GetExpressCheckoutDetailsResponseType resp =
                BuildPayPalWebservice().GetExpressCheckoutDetails(req);
            HandleError(resp);

            PayPalAPIHelper.PayPalWS.GetExpressCheckoutDetailsResponseDetailsType respDetails = resp.GetExpressCheckoutDetailsResponseDetails;

            amount = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Amount.Value;
            currecny = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Amount.currencyID.ToString();
            itemName = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Name;
            itemNumber = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Number;
            orderDescription = respDetails.PaymentDetails[0].OrderDescription;
            quantity = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Quantity;
            payerDetails = string.Format("PayerDetails:FirstName={0};LastName={1};Phone{2};", respDetails.PayerInfo.PayerName.FirstName, respDetails.PayerInfo.PayerName.LastName, respDetails.PayerInfo.ContactPhone);
        }

        private static void GetExpressCheckoutDetailsSandBox(string token, out string amount, out string currecny, out string itemName, out string itemNumber, out string orderDescription, out string quantity, out string payerDetails)
        {
            string result = string.Empty;
            // build getdetails request
            PayPalAPIHelper.PayPalSandboxWS.GetExpressCheckoutDetailsReq req = new PayPalAPIHelper.PayPalSandboxWS.GetExpressCheckoutDetailsReq()
            {
                GetExpressCheckoutDetailsRequest = new PayPalAPIHelper.PayPalSandboxWS.GetExpressCheckoutDetailsRequestType()
                {
                    Version = Version,
                    Token = token
                }
            };

            // query PayPal for transaction details
            PayPalAPIHelper.PayPalSandboxWS.GetExpressCheckoutDetailsResponseType resp =
                BuildPayPalSandboxWebservice().GetExpressCheckoutDetails(req);
            HandleErrorSandbox(resp);

            PayPalAPIHelper.PayPalSandboxWS.GetExpressCheckoutDetailsResponseDetailsType respDetails = resp.GetExpressCheckoutDetailsResponseDetails;

            amount = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Amount.Value;
            currecny = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Amount.currencyID.ToString();
            itemName = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Name;
            itemNumber = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Number;
            orderDescription = respDetails.PaymentDetails[0].OrderDescription;
            quantity = respDetails.PaymentDetails[0].PaymentDetailsItem[0].Quantity;
            payerDetails = string.Format("PayerDetails:FirstName={0};LastName={1};Phone{2};", respDetails.PayerInfo.PayerName.FirstName, respDetails.PayerInfo.PayerName.LastName, respDetails.PayerInfo.ContactPhone);
        }
        #endregion

        #region DoExpressCheckoutPayment
        public static bool DoExpressCheckoutPayment(PayPalEnvironment Environment, string expressCheckoutToken, string expressCheckoutPayerID, string orderTotalValue, PayPalCurrency currency)
        {
            if (Environment == PayPalEnvironment.SandBox)
            {
                return DoExpressCheckoutPaymentSandBox(expressCheckoutToken, expressCheckoutPayerID, orderTotalValue, currency);
            }
            else
            {
                return DoExpressCheckoutPaymentReal(expressCheckoutToken, expressCheckoutPayerID, orderTotalValue, currency);
            }
        }

        private static bool DoExpressCheckoutPaymentReal(string expressCheckoutToken, string expressCheckoutPayerID, string orderTotalValue, PayPalCurrency currency)
        {
            bool success = false;
            PayPalAPIHelper.PayPalWS.PaymentDetailsType paymentDetails = new PayPalWS.PaymentDetailsType();
            paymentDetails.OrderTotal = new PayPalWS.BasicAmountType();
            paymentDetails.OrderTotal.currencyID = ConvertProgramCurrencyToPayPal(currency);
            paymentDetails.OrderTotal.Value = orderTotalValue;

            // prepare for commiting transaction
            PayPalAPIHelper.PayPalWS.DoExpressCheckoutPaymentReq payReq = new PayPalAPIHelper.PayPalWS.DoExpressCheckoutPaymentReq()
            {
                DoExpressCheckoutPaymentRequest = new PayPalAPIHelper.PayPalWS.DoExpressCheckoutPaymentRequestType()
                {
                    Version = Version,
                    DoExpressCheckoutPaymentRequestDetails = new PayPalAPIHelper.PayPalWS.DoExpressCheckoutPaymentRequestDetailsType()
                    {

                        Token = expressCheckoutToken,
                        PaymentAction = PayPalAPIHelper.PayPalWS.PaymentActionCodeType.Sale,
                        PayerID = expressCheckoutPayerID,
                        PaymentDetails = new PayPalAPIHelper.PayPalWS.PaymentDetailsType[] { paymentDetails },
                    }
                }
            };

            // commit transaction and display results to user
            PayPalAPIHelper.PayPalWS.DoExpressCheckoutPaymentResponseType doResponse =
                BuildPayPalWebservice().DoExpressCheckoutPayment(payReq);
            HandleError(doResponse);
            success = true;

            return success;
        }

        private static bool DoExpressCheckoutPaymentSandBox(string expressCheckoutToken, string expressCheckoutPayerID, string orderTotalValue, PayPalCurrency currency)
        {
            bool success = false;
            PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsType paymentDetails = new PayPalSandboxWS.PaymentDetailsType();
            paymentDetails.OrderTotal = new PayPalSandboxWS.BasicAmountType();
            paymentDetails.OrderTotal.currencyID = ConvertProgramCurrencyToPayPalSandbox(currency);
            paymentDetails.OrderTotal.Value = orderTotalValue;

            // prepare for commiting transaction
            PayPalAPIHelper.PayPalSandboxWS.DoExpressCheckoutPaymentReq payReq = new PayPalAPIHelper.PayPalSandboxWS.DoExpressCheckoutPaymentReq()
            {
                DoExpressCheckoutPaymentRequest = new PayPalAPIHelper.PayPalSandboxWS.DoExpressCheckoutPaymentRequestType()
                {
                    Version = Version,
                    DoExpressCheckoutPaymentRequestDetails = new PayPalAPIHelper.PayPalSandboxWS.DoExpressCheckoutPaymentRequestDetailsType()
                    {

                        Token = expressCheckoutToken,
                        PaymentAction = PayPalAPIHelper.PayPalSandboxWS.PaymentActionCodeType.Sale,
                        PayerID = expressCheckoutPayerID,
                        PaymentDetails = new PayPalAPIHelper.PayPalSandboxWS.PaymentDetailsType[] { paymentDetails },
                    }
                }
            };

            // commit transaction and display results to user
            PayPalAPIHelper.PayPalSandboxWS.DoExpressCheckoutPaymentResponseType doResponse =
                BuildPayPalSandboxWebservice().DoExpressCheckoutPayment(payReq);
            HandleErrorSandbox(doResponse);
            success = true;

            return success;
        }

        #endregion

        #region Version
        private static string Version
        {
            get { return "63.0"; }
        }
        #endregion

        #region ConvertProgramCurrency

        private static PayPalAPIHelper.PayPalWS.CurrencyCodeType ConvertProgramCurrencyToPayPal(PayPalCurrency currency)
        {
            return (PayPalAPIHelper.PayPalWS.CurrencyCodeType)Enum.Parse(typeof(PayPalAPIHelper.PayPalWS.CurrencyCodeType),
                              Enum.GetName(typeof(PayPalCurrency), currency));
        }
        
        private static PayPalAPIHelper.PayPalSandboxWS.CurrencyCodeType ConvertProgramCurrencyToPayPalSandbox(PayPalCurrency currency)
        {
            return (PayPalAPIHelper.PayPalSandboxWS.CurrencyCodeType)Enum.Parse(typeof(PayPalAPIHelper.PayPalSandboxWS.CurrencyCodeType),
                              Enum.GetName(typeof(PayPalCurrency), currency));
        }

        #endregion

        #region BuildPayPalWebservice
        private static PayPalAPIHelper.PayPalWS.PayPalAPIAASoapBinding BuildPayPalWebservice()
        {
            // more details on https://www.paypal.com/en_US/ebook/PP_APIReference/architecture.html
            PayPalAPIHelper.PayPalWS.UserIdPasswordType credentials = new PayPalAPIHelper.PayPalWS.UserIdPasswordType()
            {
                Username = ConfigurationManager.AppSettings["PayPalAPIUserName"],
                Password = ConfigurationManager.AppSettings["PayPalAPIPassword"],
                Signature = ConfigurationManager.AppSettings["PayPalAPISignature"],
            };

            PayPalAPIHelper.PayPalWS.PayPalAPIAASoapBinding paypal = new PayPalAPIHelper.PayPalWS.PayPalAPIAASoapBinding();
            paypal.RequesterCredentials = new PayPalAPIHelper.PayPalWS.CustomSecurityHeaderType()
            {
                Credentials = credentials
                
            };

            return paypal;
        }

        private static PayPalAPIHelper.PayPalSandboxWS.PayPalAPIAASoapBinding BuildPayPalSandboxWebservice()
        {
            // more details on https://www.paypal.com/en_US/ebook/PP_APIReference/architecture.html
            PayPalAPIHelper.PayPalSandboxWS.UserIdPasswordType credentials = new PayPalAPIHelper.PayPalSandboxWS.UserIdPasswordType()
            {
                Username = ConfigurationManager.AppSettings["PayPalSandboxAPIUserName"],
                Password = ConfigurationManager.AppSettings["PayPalSandboxAPIPassword"],
                Signature = ConfigurationManager.AppSettings["PayPalSandboxAPISignature"],
            };

            PayPalAPIHelper.PayPalSandboxWS.PayPalAPIAASoapBinding paypal = new PayPalAPIHelper.PayPalSandboxWS.PayPalAPIAASoapBinding();
            paypal.RequesterCredentials = new PayPalAPIHelper.PayPalSandboxWS.CustomSecurityHeaderType()
            {
                Credentials = credentials
            };

            return paypal;
        }

        #endregion

        #region HandleError
        private static void HandleError(PayPalAPIHelper.PayPalWS.AbstractResponseType resp)
        {
            if (resp.Errors != null && resp.Errors.Length > 0)
            {
                string errMessage = "Exception(s) occured when calling PayPal. First exception: " + resp.Errors[0].LongMessage;
                // Optional : Write to Log the error
                throw new Exception(errMessage);
            }
        }

        private static void HandleErrorSandbox(PayPalAPIHelper.PayPalSandboxWS.AbstractResponseType resp)
        {
            if (resp.Errors != null && resp.Errors.Length > 0)
            {
                string errMessage = "Exception(s) occured when calling PayPal. First exception: " + resp.Errors[0].LongMessage;
                // Optional : Write to Log the error
                throw new Exception(errMessage);
            }
        }
        #endregion
    }
}
