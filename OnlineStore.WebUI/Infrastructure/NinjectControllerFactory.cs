using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OnlineStore.Domain.Concrete;
using System.Configuration;
using OnlineStore.WebUI.Infrastructure.Abstract;
using OnlineStore.WebUI.Infrastructure.Concrete;


namespace OnlineStore.WebUI.Infrastructure
{
    // реализация пользовательской фабрики контроллеров
    // наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // получение объекта контроллера из контейнера
            // используя его тип
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}