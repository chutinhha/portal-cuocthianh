using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Helper
{
   public static class ConvertObject
    {


       public static string ToString(object value)
       {
           if (value == null)
               return "";
           return value.ToString();
       }
       public static int ToInt(object value)
       {
           try
           {
               if (value == null)
                   return 0;
               return int.Parse(value.ToString());
           }
           catch { return 0; }
       }
       public static long ToLong(object value)
       {
           try
           {
               if (value == null)
                   return 0;
               return long.Parse(value.ToString());
           }
           catch { return 0; }
       }
       public static bool ToBool(object value)
       {
           try
           {
               if (value.ToString() == "1")
               {
                   return true;
               }
               if (value.ToString() == "0")
               {
                   return false;
               }
               return bool.Parse(value.ToString());
           }
           catch { return false; }
       }

       public static Decimal ToDecimal(object value)
       {
           try
           {
               if (value == null)
                   return 0;
               return Decimal.Parse(value.ToString());
           }
           catch { return 0; }
       }
       public static float ToFloat(object value)
       {
           try
           {
               if (value == null)
                   return 0;
               return float.Parse(value.ToString());
           }
           catch { return 0; }
       }
       public static Guid ToGuid(object value)
       {
           try
           {
               if (value == null)
                   return Guid.Empty;
               return new Guid(value.ToString());
           }
           catch { return Guid.Empty; }
       }

       public static DateTime ToDateTime(object value)
       {
           try
           {
               if (value == null)
                   return DateTime.MinValue;
               return DateTime.Parse(value.ToString());
           }
           catch { return DateTime.MinValue; }
       }


       /// <summary>
       /// Convert Class To Object
       /// </summary>
       /// <param name="c"></param>
       /// <returns></returns>
       public static object ConvertToObject(object c)
       {

           object _c = c;
           return _c;
       }


       /// <summary>
       /// Convert Model To Entity
       /// </summary>
       /// <param name="_model"></param>
       /// <param name="_entity"></param>
       public static void ConvertModelToEntity(Object _model, ref Object _entity)
       {
           try
           {
               Type typemodel = _model.GetType();
               PropertyInfo[] propertiesmodel = typemodel.GetProperties();
               Type type_entity = _entity.GetType();
               PropertyInfo[] propertiesentity = type_entity.GetProperties();
               for (int i = 0; i < propertiesmodel.Length; i++)
               {
                   try
                   {
                       for (int j = 0; j < propertiesentity.Length; j++)
                           if (propertiesmodel[i].Name == propertiesentity[j].Name)
                           {
                               var value = propertiesmodel[i].GetValue(_model, null);
                               propertiesentity[j].SetValue(_entity, value, null);
                           }
                   }
                   catch { }
               }
              
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       /// <summary>
       /// Convert Entity To Model
       /// </summary>
       /// <param name="_model"></param>
       /// <param name="_entity"></param>
       public static void ConvertEntityToModel(Object _entity, ref Object _model)
       {
           try
           {
               Type typeentity = _entity.GetType();
               PropertyInfo[] propertiesentity = typeentity.GetProperties();
               Type type_model = _model.GetType();
               PropertyInfo[] propertiesmodel = type_model.GetProperties();
               for (int i = 0; i < propertiesentity.Length; i++)
               {
                   try
                   {
                       for (int j = 0; j < propertiesmodel.Length; j++)
                           if (propertiesmodel[j].Name == propertiesentity[i].Name)
                           {
                               var value = propertiesentity[i].GetValue(_entity, null);
                               propertiesmodel[j].SetValue(_model, value, null);
                               break;
                           }
                   }
                   catch { }
               }
           }
           catch
           { }

       }


    }
}
