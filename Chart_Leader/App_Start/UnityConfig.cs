using Chart_Business_Layers;
using Chart_Business_Layers.Interface;
using Chart_Leader.Controllers;
using Chart_Leader.Repository.RepositoryS_;
using Microsoft.AspNet.Identity;
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




            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));//Important
                                                                                       //container.RegisterType<IproductsBusiness, ProductBusiness>();
            container.RegisterType<IproductsBusiness, ProductBusiness>();
            container.RegisterType<IUnitOfWork, GenericUnitOfWork>();


            
            //Login and Registerattion
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());




            //container.RegisterType<IUnitOfWork, UnitOfWork>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}