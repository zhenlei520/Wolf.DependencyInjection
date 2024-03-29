﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Wolf.DependencyInjection.Autofac.Extension
{
    /// <summary>
    ///
    /// </summary>
    public class AutofacAutoRegister
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual IServiceProvider Build(IServiceCollection serviceCollection, Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action?.Invoke(builder);
            builder.Populate(serviceCollection);
            var container = builder.Build();
            var servicesProvider = new AutofacServiceProvider(container);
            return servicesProvider;
        }
    }
}
