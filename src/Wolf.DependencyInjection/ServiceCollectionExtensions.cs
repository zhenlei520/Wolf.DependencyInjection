// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Wolf.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="implementationInstance">实现</param>
        /// <param name="serviceLifetime"></param>
        /// <typeparam name="TService">接口</typeparam>
        public static IServiceCollection TryAddEnumerable<TService>(this IServiceCollection serviceCollection,
            TService implementationInstance,
            ServiceLifetime serviceLifetime)
            where TService : class
        {
            if (serviceLifetime != ServiceLifetime.Singleton && serviceLifetime != ServiceLifetime.Scoped &&
                serviceLifetime != ServiceLifetime.Transient)
            {
                throw new NotSupportedException(nameof(serviceLifetime));
            }

            return serviceCollection.TryAddEnumerable(typeof(TService), implementationInstance.GetType(),
                serviceLifetime);
        }

        /// <summary>
        /// 匹配接口与实现类不一致时添加，否则不添加
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceLifetime">生命周期</param>
        /// <typeparam name="TService">接口</typeparam>
        /// <typeparam name="TImplementation">实现</typeparam>
        public static IServiceCollection TryAddEnumerable<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.TryAddEnumerable(typeof(TService), typeof(TImplementation), serviceLifetime);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="service">接口</param>
        /// <param name="implementation">实现</param>
        /// <param name="serviceLifetime">生命周期</param>
        public static IServiceCollection TryAddEnumerable(this IServiceCollection serviceCollection,
            Type service,
            Type implementation,
            ServiceLifetime serviceLifetime)
        {
            serviceCollection.TryAddEnumerable(ServiceDescriptor.Describe(service, implementation, serviceLifetime));
            return serviceCollection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <typeparam name="TService">接口</typeparam>
        /// <returns></returns>
        public static IServiceCollection RemoveAll<TService>(this IServiceCollection serviceCollection) where TService : class
        {
            return ServiceCollectionDescriptorExtensions.RemoveAll<TService>(serviceCollection);
        }
    }
}
