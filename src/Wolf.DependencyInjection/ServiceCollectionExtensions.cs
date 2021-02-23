// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
        /// <param name="services"></param>
        /// <param name="implementationInstance"></param>
        /// <typeparam name="TService"></typeparam>
        public static void TryAddEnumerable<TService>(this IServiceCollection services, TService implementationInstance)
            where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddEnumerable(services,
                ServiceDescriptor.Singleton(implementationInstance));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceLifetime">生命周期</param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public static void TryAddEnumerable<TService, TImplementation>(this IServiceCollection services,
            ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceCollectionDescriptorExtensions.TryAddEnumerable(services,
                ServiceDescriptor.Describe(typeof(TService), typeof(TImplementation), serviceLifetime));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceCollection RemoveAll<TService>(this IServiceCollection services) where TService : class
        {
            return ServiceCollectionDescriptorExtensions.RemoveAll<TService>(services);
        }
    }
}
