using Autofac;
using CarClassified.Common.Interfaces;

namespace CarClassified.Common.IOCModule
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CarClassified.Common.IOCModule.CustomModule" />
    public class CommonIoc : CustomModule
    {
        /// <summary>
        /// Loads the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenUtility>().As<ITokenUtility>();
        }
    }

    /// <summary>
    /// Implements Aufoc module configuration
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class CustomModule : Module
    {
    }
}
