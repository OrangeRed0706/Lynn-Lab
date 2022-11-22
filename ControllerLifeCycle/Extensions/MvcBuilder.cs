using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ControllerLifeCycle.Extensions
{
    public static class MvcBuilder
    {
        public static IMvcBuilder AddCustomerControllersAsServices(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var feature = new ControllerFeature();
            builder.PartManager.PopulateFeature(feature);

            foreach (var controller in feature.Controllers.Select(c => c.AsType()))
            {
                builder.Services.TryAddSingleton(controller, controller);
            }

            builder.Services.Replace(ServiceDescriptor.Singleton<IControllerActivator, ServiceBasedControllerActivator>());

            return builder;
        }
    }
}
