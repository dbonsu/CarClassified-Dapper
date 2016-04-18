using Autofac;
using CarClassified.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Common.IOCModule
{
    public class CommonIoc : CustomModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenUtility>().As<ITokenUtility>();
        }
    }

    public class CustomModule : Module
    {
    }
}