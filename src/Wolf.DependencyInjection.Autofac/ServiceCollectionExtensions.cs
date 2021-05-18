// Copyright (c) zhenlei520 All rights reserved.

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection.Autofac.Extension;

namespace Wolf.DependencyInjection.Autofac
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="packageNamePrefix">多个包前缀注入</param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            params string[] packageNamePrefix)
        {
            return serviceCollection.Build(null, packageNamePrefix);
        }

        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <param name="packageNamePrefix">多个包前缀注入</param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            Action<ContainerBuilder> action = null, params string[] packageNamePrefix)
        {
            serviceCollection.AddAutoInject(packageNamePrefix);
            return serviceCollection.Build(action);
        }

        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            Action<ContainerBuilder> action = null)
        {
            serviceCollection.AddAutofac(action);
            return new AutofacAutoRegister().Build(serviceCollection, action);
        }
    }
}
