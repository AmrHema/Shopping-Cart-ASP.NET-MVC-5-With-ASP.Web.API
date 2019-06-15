using Chart_Business_Layers;
using Chart_Business_Layers.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Chart_Leader
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();




            
            container.RegisterType<ProductBusiness>(new InjectionConstructor(0));
            container.RegisterType<IproductsBusiness, ProductBusiness>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}