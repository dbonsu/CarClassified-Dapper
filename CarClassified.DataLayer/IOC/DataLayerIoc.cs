using Autofac;
using CarClassified.Common;
using CarClassified.Common.IOCModule;
using CarClassified.DataLayer.Base;
using CarClassified.DataLayer.Interfaces;

namespace CarClassified.DataLayer.IOC
{
    /// <summary>
    /// Data layer IOC
    /// </summary>
    /// <seealso cref="CarClassified.Common.IOCModule.CustomModule" />
    public class DataLayerIoc : CustomModule
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
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
