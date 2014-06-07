using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
   public class SQLCommand
   {
       public const string data =" select * from where ID ={0}";



       #region Model
       public const string GetAllWithManufacturerName = " select Model.*, Manufacturer.Name As ManufacturerName from Model INNER JOIN Manufacturer ON Manufacturer.ID =Model.ManufacturerID";
       #endregion

       #region User
       public const string GetUserByGroup = " select Users.*, User_Group.GroupID from Users, User_Group where Users.ID=User_Group.UserID and User_Group.GroupID={0}";
       public const string GetUserByID = " select Users.*, User_Group.GroupID from Users, User_Group where Users.ID=User_Group.UserID and Users.ID={0}";
       public const string SearchUser = @" select Users.*, User_Group.GroupID from Users, User_Group
                                           where Users.ID=User_Group.UserID and User_Group.GroupID={0}  {1}  {2}  {3}";
       public const string GetUserByUserName = @" select Users.* from Users where Users.ID=(select Users.ID from Users where Users.UserName = '{0}')";
       public const string GetUserByEmail = @" select Users.* from Users where Users.ID=(select Users.ID from Users where Users.Email = '{0}')";
       #endregion

       #region Product

       public const string GetProductList = @" select Product.*, Category.Name AS CategoryName
        , Manufacturer.Name AS ManufacturerName, ProductType.Name AS ProductTypeName 
         from Product, Category, Manufacturer, ProductType where Product.CateID=Category.ID and Product.ProductTypeID = ProductType.ID
              and Product.ManufacturerID =Manufacturer.ID";

       public const string SearchProduct = @"select Product.*, Category.Name AS CategoryName
                                           , Manufacturer.Name AS ManufacturerName, ProductType.Name AS ProductTypeName 
                                             from Product, Category, Manufacturer, ProductType 
                                             where Product.CateID=Category.ID and Product.ProductTypeID = ProductType.ID
                                                   and Product.ManufacturerID =Manufacturer.ID
                                               {0}";
       public const string SearchProductOnHome = @"select Product.* , Manufacturer.Name AS ManufacturerName
                                             from Product, Manufacturer 
                                             where  Product.ManufacturerID =Manufacturer.ID and Product.Active ='True' {0}";

       public const string GetListProductByCategoryId = @"select Product.*, Category.Name AS CategoryName
                                           , Manufacturer.Name AS ManufacturerName, ProductType.Name AS ProductTypeName 
                                             from Product, Category, Manufacturer, ProductType 
                                             where Product.CateID={0} and Product.CateID=Category.ID and Product.ProductTypeID = ProductType.ID
                                                   and Product.ManufacturerID =Manufacturer.ID";

       #endregion

       #region Product Attribute

       public const string GetListProductAttribute = @" select ProductAttribute.*, Attribute.Name AS AttributeName from ProductAttribute, Attribute
                                                        where ProductAttribute.AttributeID = Attribute.ID and ProductAttribute.ProductID ={0}";

       #endregion

       #region Order

       //  public const string GetOrder = " select Order.* from Users, Order where Users.ID = Order.UserID";
       public const string GetOrderByUserID = "select * from [Order] where Active=1 and UserID = '{0}'";

       #endregion

       #region OrderDetail

       public const string GetOrderDetailByID = @" select OrderDetail.*, Product.Name AS ProductName from OrderDetail, Product
                                                  where OrderDetail.ProductID = Product.ID and OrderDetail.OrderID ={0}";
       public const string GetOrderDetailWithUserIDByOrderID = @" select D.*,O.Name AS Name,O.Address, O.Phone, O.CreateDate, O.UserID,P.Name AS ProductName, P.Link AS ProductLink from OrderDetail D
left join [Order] O on O.ID=D.OrderID left join Product P on P.ID=D.ProductID where OrderID={0} AND O.UserID={1}";

       #endregion

       #region Article
      
       /// <summary>
       /// Chưa hiểu lắm phần này, nếu đã gán tĩnh Article Category thì làm sao
       /// </summary>
       //public const string GetArticleList = @" select Article.* FROM Article";
       public const string GetArticleList = @" select Article.*, Users.ID AS UserID, Users.Name AS UserName
                                                from Article, Users
                                                where Article.UserID = Users.ID";
       //public const string GetListByRelationsID = @" select * from Article {0} {1}";
       public const string GetListByRelationsID = @" select a.ID, a.Active, a.CateID, a.Image, a.Link,A.ShortDescription,
    a.ShowHomePage,a.Title,a.UpdateDate, a.UserID AS UserID {0}, U.UserName AS UserAccount
    from Article A, Users U
    WHERE a.UserID=u.ID {1}";

       public const string GetArticleSameListByCateID = @" SELECT top {1} *
                                        FROM Article
                                        tablesample ({1}+{1} rows)
                                        WHERE Article.CateID =(select CateID from Article where ID={0}) and ID <> {0}";
       
       #endregion

       #region District
       public const string GetListDistrict = @" select District.*, Province.Name AS ProvinceName from District 
                                                 INNER JOIN  Province on Province.ID = District.ProvinceID";
       public const string GetListDicTrictByProvince = @" select District.*, Province.Name AS ProvinceName from District 
                                                 INNER JOIN  Province on Province.ID = District.ProvinceID and District.ProvinceID = {0}";

       #endregion

       #region User - Role
       public const string GetUserRole = @"select  User_Role_Module.ID, User_Role_Module.UserID,User_Role_Module.ModuleID, UserName,[Group].Name AS GroupName, Module.Name AS ModuleName,User_Role_Module.Role   from Users,User_Group,[Group], Module, User_Role_Module 
                                             where Users.ID = User_Group.UserID and User_Group.GroupID = [Group].ID and
                                                Module.ID = User_Role_Module.ModuleID and  [Group].ID =2 and Users.ID = User_Role_Module.UserID {0}";
       #endregion


       #region Configuration

       public const string GetGenneralConfig = @" select * from Configuration where Code ='Title' or Code ='Metakeyword'
                                                   or Code ='Metadescription' or Code ='Facebook' ";
       public const string GetEmailConfig = @" select * from Configuration where Code ='Email' or Code ='EmailPassword'";

       #endregion



       #region Customer Idea
       public const string GetCustomerIdeaByType = @"select distinct C.*,CASE C.UserID WHEN 0  
                                                    THEN  'Anonymous'
                                                    ELSE U.UserName END AS UserName 
                                                    FROM CustomerIdea C LEFT JOIN Users U on U.ID =c.UserID
                                                    WHERE  C.Type={0}";

       #endregion

   }
}
