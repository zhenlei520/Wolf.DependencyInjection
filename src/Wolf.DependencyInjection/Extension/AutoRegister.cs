using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection.Abstracts;
using Wolf.Systems.Core.Common;

namespace Wolf.DependencyInjection.Extension
{
    /// <summary>
    ///
    /// </summary>
    public class AutoRegister
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// 不建议使用当前程序集域，因为依赖注入采用惰性加载，会导致所需要的应用程序集确实
        /// </summary>
        public AutoRegister() : this(AppDomain.CurrentDomain.GetAssemblies())
        {
        }

        /// <summary>
        ///
        /// </summary>
        public AutoRegister(Assembly[] assemblies)
        {
            this._assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddAutoInject(IServiceCollection serviceCollection)
        {
            #region 单例

            RegisterGeneric(serviceCollection, typeof(ISingleInstance), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
            RegisterGeneric(serviceCollection, typeof(ISingleInstance<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);

            #endregion

            #region 请求

            RegisterGeneric(serviceCollection, typeof(IPerRequest), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
            RegisterGeneric(serviceCollection, typeof(IPerRequest<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);

            #endregion

            #region 瞬时

            RegisterGeneric(serviceCollection, typeof(IDependency), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
            RegisterGeneric(serviceCollection, typeof(IDependency<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);

            #endregion

            return serviceCollection;
        }

        #region 添加注入

        /// <summary>
        /// 添加注入
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="type"></param>
        /// <param name="serviceLifetime"></param>
        private void RegisterGeneric(IServiceCollection serviceCollection, Type type, ServiceLifetime serviceLifetime)
        {
            var list = TypeCommon.GetInterfaceAndImplementationType(this._assemblies, type);
            foreach (var item in list)
            {
                serviceCollection.TryAddEnumerable(item.Key, item.Value, serviceLifetime);
            }
        }

        #endregion
    }
}
