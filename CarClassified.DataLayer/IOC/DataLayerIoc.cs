using Autofac;
using CarClassified.Common;
using CarClassified.Common.IOCModule;
using CarClassified.DataLayer.Base;
using CarClassified.DataLayer.Interfaces;

namespace CarClassified.DataLayer.IOC
{
    public class DataLayerIoc : CustomModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ICarClassifiedContext>(c =>
               new CarClassifiedContext(BaseSettings.ConnectionString)
                ).InstancePerDependency();
            builder.RegisterType<Database>().As<IDatabase>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}