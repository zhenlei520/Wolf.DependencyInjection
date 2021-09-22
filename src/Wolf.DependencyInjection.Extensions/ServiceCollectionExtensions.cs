// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region 接口添加后就不再添加

        /// <summary>
        /// 接口添加后就不再添加
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAdd(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime serviceLifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            services.TryAdd(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
            return services;
        }

        public static IServiceCollection TryAdd(this IServiceCollection services, Type serviceType, ServiceLifetime serviceLifetime)
        {
            services.TryAdd(serviceType, serviceType, serviceLifetime);
            return services;
        }

        public static IServiceCollection TryAdd<TService>(this IServiceCollection services, ServiceLifetime serviceLifetime)
            where TService : class
        {
            services.TryAdd(typeof(TService), serviceLifetime);
            return services;
        }

        public static IServiceCollection TryAdd<TService, TImplementation>(this IServiceCollection services, ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            services.TryAdd(typeof(TService), typeof(TImplementation), serviceLifetime);
            return services;
        }

        public static IServiceCollection TryAdd(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime serviceLifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            services.TryAdd(new ServiceDescriptor(serviceType, implementationFactory, serviceLifetime));
            return services;
        }

        public static IServiceCollection TryAdd<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, ServiceLifetime serviceLifetime)
            where TService : class
        {
            services.TryAdd(new ServiceDescriptor(typeof(TService), implementationFactory, serviceLifetime));
            return services;
        }

        #endregion

        #region 匹配接口与实现类不一致时添加，否则不添加

        /// <summary>
        /// 匹配接口与实现类不一致时添加，否则不添加
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceLifetime"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public static IServiceCollection TryAddEnumerable<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceCollection.TryAddEnumerable(typeof(TService), typeof(TImplementation), serviceLifetime);
        }

        /// <summary>
        /// 匹配接口与实现类不一致时添加，否则不添加
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection TryAddEnumerable<TService>(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            return services.TryAddEnumerable(typeof(TService), serviceLifetime);
        }

        /// <summary>
        /// 匹配接口与实现类不一致时添加，否则不添加
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceType"></param>
        /// <param name="serviceLifetime"></param>
        public static IServiceCollection TryAddEnumerable(this IServiceCollection services, Type serviceType, ServiceLifetime serviceLifetime)
        {
            return services.TryAddEnumerable(serviceType, serviceType, serviceLifetime);
        }

        /// <summary>
        /// 匹配接口与实现类不一致时添加，否则不添加
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="serviceLifetime"></param>
        public static IServiceCollection TryAddEnumerable(this IServiceCollection serviceCollection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime serviceLifetime)
        {
            if (!((int)serviceLifetime).IsExist<ServiceLifetime>())
            {
                throw new NotSupportedException(nameof(serviceLifetime));
            }

            serviceCollection.TryAddEnumerable(ServiceDescriptor.Describe(serviceType, implementationType, serviceLifetime));
            return serviceCollection;
        }

        #endregion
    }
}
