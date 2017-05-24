using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Magrathea;
using Magrathea.Interfaces;
using MyTransactions.Core;
using MyTransactions.Models;

namespace MyTransactions.Installers
{
    public class WindsorCustomInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRepository>().ImplementedBy<Repository>(),
                Component.For<DbContext>().ImplementedBy<DataContext>()
            );
        }
    }
}