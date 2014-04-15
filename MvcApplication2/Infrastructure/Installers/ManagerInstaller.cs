using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Infrastructure.Installers
{
    public class ManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                       .Where(type => type.Name.EndsWith("Manager"))
                       .WithServiceDefaultInterfaces()
                       .Configure(c => c.LifestyleTransient())
            );
        }
    }
}