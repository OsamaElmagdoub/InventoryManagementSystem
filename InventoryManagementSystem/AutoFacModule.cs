using Autofac;
using InventoryManagementSystem.Data;

namespace InventoryManagementSystem
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerLifetimeScope(); 
        }
    }
}
