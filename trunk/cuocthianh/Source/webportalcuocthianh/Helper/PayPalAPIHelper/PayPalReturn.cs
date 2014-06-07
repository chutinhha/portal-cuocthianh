using System;
using System.Data;
using System.Configuration;
using System.Web;

public struct PayPalReturn
{
    public bool IsSucess;
    public string ErrorMessage;
    public string TransactionID;
    public object ObjectValue;
    public string errorcode;
}
