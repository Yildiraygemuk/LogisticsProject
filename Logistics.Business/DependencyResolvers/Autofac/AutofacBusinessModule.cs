using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Logistics.Core.Utilities.Interceptors;
using Logistics.DataAccess;
using Logistics.DataAccess.Abstract;

namespace Logistics.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<OrderService>().As<IOrderService>();

            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();

            var assemblyBusiness = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyDataAccess = System.Reflection.Assembly.Load("Logistics.DataAccess");

            builder.RegisterAssemblyTypes(assemblyBusiness, assemblyDataAccess).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                });
        }
    }
}