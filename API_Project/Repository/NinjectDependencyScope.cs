using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using API_Project.Models;
using Ninject;
using Ninject.Syntax;

namespace API_Project.Repository
{
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.Bind<IRepository<Product>>().To<GenericRepository<Product>>();
        }

        public NinjectDependencyResolver( )
           : this(new StandardKernel())
        {
        }
         
        public IDependencyScope BeginScope()
        {
            //return new NinjectDependencyScope(kernel.BeginBlock());
            return this;

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
           
        }
    }
}