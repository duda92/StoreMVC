using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.Domain.Concrete;
using System.Configuration;
using Store.Administration;
using System.Web.Routing;
using System.Threading;
using System.Globalization;

namespace Store.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            ninjectKernel = kernel;
            AddBinding();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
           return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBinding()
        {
            ninjectKernel.Bind<IOrderProcessor>().To<SimpleOrderProcessor>();
        }

    }
}
