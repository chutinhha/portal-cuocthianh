using System;
using System.Web.Mvc;
namespace RegisterService
{
    public class CustomControllerActivator : IControllerActivator
    {        
        IController IControllerActivator.Create(
            System.Web.Routing.RequestContext requestContext,
            Type controllerType)
        {
            return System.Web.Mvc.DependencyResolver.Current
                .GetService(controllerType) as IController;
        }      
    }
}