using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using ActionServices;
using EntityData;
using RegisterService;
using System.Web.Mvc;
namespace RegisterService
{
   public class RegisterServices
    {
       public static void RegisterDependencyResolver()
       {
           var container = new UnityContainer();

           container.RegisterType<IAttributeEntity, AttributeEntity>(new HttpContextLifetimeManager<IAttributeEntity>());
           container.RegisterType<IAttributeActionService, AttributeActionService>(new HttpContextLifetimeManager<IAttributeActionService>());
           container.RegisterType<IBanner_LogoEntity, Banner_LogoEntity>(new HttpContextLifetimeManager<IBanner_LogoEntity>());
           container.RegisterType<IBanner_LogoActionService, Banner_LogoActionService>(new HttpContextLifetimeManager<IBanner_LogoActionService>());
           container.RegisterType<ICategoryEntity, CategoryEntity>(new HttpContextLifetimeManager<ICategoryEntity>());
           container.RegisterType<ICategoryActionService, CategoryActionService>(new HttpContextLifetimeManager<ICategoryActionService>());


           container.RegisterType<IConfigurationEntity, ConfigurationEntity>(new HttpContextLifetimeManager<IConfigurationEntity>());
           container.RegisterType<IConfigurationActionService, ConfigurationActionService>(new HttpContextLifetimeManager<IConfigurationActionService>());


           container.RegisterType<ICustomerIdeaEntity, CustomerIdeaEntity>(new HttpContextLifetimeManager<ICustomerIdeaEntity>());
           container.RegisterType<ICustomerIdeaActionService, CustomerIdeaActionService>(new HttpContextLifetimeManager<ICustomerIdeaActionService>());


           container.RegisterType<IDistrictEntity, DistrictEntity>(new HttpContextLifetimeManager<IDistrictEntity>());
           container.RegisterType<IDistrictActionService, DistrictActionService>(new HttpContextLifetimeManager<IDistrictActionService>());


           container.RegisterType<IGroupEntity, GroupEntity>(new HttpContextLifetimeManager<IGroupEntity>());
           container.RegisterType<IGroupActionService, GroupActionService>(new HttpContextLifetimeManager<IGroupActionService>());


           container.RegisterType<IGroup_Role_ModuleEntity, Group_Role_ModuleEntity>(new HttpContextLifetimeManager<IGroup_Role_ModuleEntity>());
           container.RegisterType<IGroup_Role_ModuleActionService, Group_Role_ModuleActionService>(new HttpContextLifetimeManager<IGroup_Role_ModuleActionService>());


           container.RegisterType<IManufacturerEntity, ManufacturerEntity>(new HttpContextLifetimeManager<IManufacturerEntity>());
           container.RegisterType<IManufacturerActionService, ManufacturerActionService>(new HttpContextLifetimeManager<IManufacturerActionService>());


           container.RegisterType<IModelEntity, ModelEntity>(new HttpContextLifetimeManager<IModelEntity>());
           container.RegisterType<IModelActionService, ModelActionService>(new HttpContextLifetimeManager<IModelActionService>());


           container.RegisterType<IModuleEntity, ModuleEntity>(new HttpContextLifetimeManager<IModuleEntity>());
           container.RegisterType<IModuleActionService, ModuleActionService>(new HttpContextLifetimeManager<IModuleActionService>());


           container.RegisterType<IOrderEntity, OrderEntity>(new HttpContextLifetimeManager<IOrderEntity>());
           container.RegisterType<IOrderActionService, OrderActionService>(new HttpContextLifetimeManager<IOrderActionService>());


           container.RegisterType<IOrderDetailEntity, OrderDetailEntity>(new HttpContextLifetimeManager<IOrderDetailEntity>());
           container.RegisterType<IOrderDetailActionService, OrderDetailActionService>(new HttpContextLifetimeManager<IOrderDetailActionService>());


           container.RegisterType<IProductEntity, ProductEntity>(new HttpContextLifetimeManager<IProductEntity>());
           container.RegisterType<IProductActionService, ProductActionService>(new HttpContextLifetimeManager<IProductActionService>());


           container.RegisterType<IProductAttributeEntity, ProductAttributeEntity>(new HttpContextLifetimeManager<IProductAttributeEntity>());
           container.RegisterType<IProductAttributeActionService, ProductAttributeActionService>(new HttpContextLifetimeManager<IProductAttributeActionService>());


           container.RegisterType<IProductTypeEntity, ProductTypeEntity>(new HttpContextLifetimeManager<IProductTypeEntity>());
           container.RegisterType<IProductTypeActionService, ProductTypeActionService>(new HttpContextLifetimeManager<IProductTypeActionService>());


           container.RegisterType<IProvinceEntity, ProvinceEntity>(new HttpContextLifetimeManager<IProvinceEntity>());
           container.RegisterType<IProvinceActionService, ProvinceActionService>(new HttpContextLifetimeManager<IProvinceActionService>());


           container.RegisterType<IUserEntity, UserEntity>(new HttpContextLifetimeManager<IUserEntity>());
           container.RegisterType<IUserActionService, UserActionService>(new HttpContextLifetimeManager<IUserActionService>());


           container.RegisterType<IUser_GroupEntity, User_GroupEntity>(new HttpContextLifetimeManager<IUser_GroupEntity>());
           container.RegisterType<IUser_GroupActionService, User_GroupActionService>(new HttpContextLifetimeManager<IUser_GroupActionService>());


           container.RegisterType<IUser_Role_ModuleEntity, User_Role_ModuleEntity>(new HttpContextLifetimeManager<IUser_Role_ModuleEntity>());
           container.RegisterType<IUser_Role_ModuleActionService, User_Role_ModuleActionService>(new HttpContextLifetimeManager<IUser_Role_ModuleActionService>());

           container.RegisterType<ISearchActionService, SearchActionService>(new HttpContextLifetimeManager<ISearchActionService>());

           container.RegisterType<IArticleEntity, ArticleEntity>(new HttpContextLifetimeManager<IArticleEntity>());
           container.RegisterType<IArticleActionService, ArticleActionService>(new HttpContextLifetimeManager<IArticleActionService>());

           container.RegisterType<IEmailTemplateEntity, EmailTemplateEntity>(new HttpContextLifetimeManager<IEmailTemplateEntity>());
           container.RegisterType<IEmailTemplateActionService, EmailTemplateActionService>(new HttpContextLifetimeManager<IEmailTemplateActionService>());

           container.RegisterType<IEmailListEntity, EmailListEntity>(new HttpContextLifetimeManager<IEmailListEntity>());
           container.RegisterType<IEmailListActionService, EmailListActionService>(new HttpContextLifetimeManager<IEmailListActionService>());

           
           container.RegisterType<IShoppingCartActionService, ShoppingCartActionService>(new HttpContextLifetimeManager<IShoppingCartActionService>());

           container.RegisterType<ICategoryArticleEntity, CategoryArticleEntity>(new HttpContextLifetimeManager<ICategoryArticleEntity>());
           container.RegisterType<ICategoryArticleActionService, CategoryArticleActionService>(new HttpContextLifetimeManager<ICategoryArticleActionService>());

           container.RegisterType<IExamineeEntity, ExamineeEntity>(new HttpContextLifetimeManager<IExamineeEntity>());
           container.RegisterType<IExamineeActionService, ExamineeActionService>(new HttpContextLifetimeManager<IExamineeActionService>());

           container.RegisterType<IPictureExamEntity, PictureExamEntity>(new HttpContextLifetimeManager<IPictureExamEntity>());
           container.RegisterType<IPictureExamActionService, PictureExamActionService>(new HttpContextLifetimeManager<IPictureExamActionService>());


           container.RegisterType<ICommentEntity, CommentEntity>(new HttpContextLifetimeManager<ICommentEntity>());
           container.RegisterType<ICommentActionService, CommentActionService>(new HttpContextLifetimeManager<ICommentActionService>());


           System.Web.Mvc.DependencyResolver.SetResolver(new DependencyResolver(container));
       }
    }
}
