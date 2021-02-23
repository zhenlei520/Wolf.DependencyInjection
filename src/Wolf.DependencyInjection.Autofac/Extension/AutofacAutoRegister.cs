using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection.Abstracts;
using Wolf.Systems.Core.Common;

namespace Wolf.DependencyInjection.Autofac.Extension
{
    /// <summary>
    ///
    /// </summary>
    public class AutofacAutoRegister
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// 不建议使用当前程序集域，因为依赖注入采用惰性加载，会导致所需要的应用程序集确实
        /// </summary>
        public AutofacAutoRegister() : this(AppDomain.CurrentDomain.GetAssemblies())
        {
        }

        /// <summary>
        ///
        /// </summary>
        public AutofacAutoRegister(Assembly[] assemblies)
        {
            this._assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual IServiceProvider Build(IServiceCollection services, Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();

            #region 单例

            var singleInstanceType = typeof(ISingleInstance);
            builder.RegisterAssemblyTypes(this._assemblies)
                .Where(t => singleInstanceType.IsAssignableFrom(t) && t != singleInstanceType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .SingleInstance();

            RegisterGeneric(builder, typeof(ISingleInstance<>));
            RegisterGeneric(builder, typeof(ISingleInstance<,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(ISingleInstance<,,,,,,,,,,,,,,,,,>));

            #endregion

            #region 请求

            var perRequestType = typeof(IPerRequest);
            action?.Invoke(builder);
            builder.RegisterAssemblyTypes(this._assemblies)
                .Where(t => perRequestType.IsAssignableFrom(t) && t != perRequestType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            RegisterGeneric(builder, typeof(IPerRequest<>));
            RegisterGeneric(builder, typeof(IPerRequest<,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IPerRequest<,,,,,,,,,,,,,,,,,>));

            #endregion

            #region 瞬时

            var perDependencyType = typeof(IDependency);
            builder.RegisterAssemblyTypes(this._assemblies)
                .Where(t => perDependencyType.IsAssignableFrom(t) && t != perDependencyType)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            RegisterGeneric(builder, typeof(IDependency<>));
            RegisterGeneric(builder, typeof(IDependency<,>));
            RegisterGeneric(builder, typeof(IDependency<,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,,,,,>));
            RegisterGeneric(builder, typeof(IDependency<,,,,,,,,,,,,,,,,,>));

            #endregion

            builder.Populate(services);
            var container = builder.Build();
            var servicesProvider = new AutofacServiceProvider(container);
            return servicesProvider;
        }

        #region 添加泛型注入

        /// <summary>
        /// 添加泛型注入
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="type"></param>
        private void RegisterGeneric(ContainerBuilder containerBuilder, Type type)
        {
            var list = TypeCommon.GetInterfaceAndImplementationType(this._assemblies, type);
            foreach (var item in list)
            {
                containerBuilder.RegisterGeneric(item.Value).As(item.Key)
                    .PropertiesAutowired().InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// 添加泛型注入
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="list">接口与实现类集合</param>
        private void RegisterGeneric(ContainerBuilder containerBuilder, List<KeyValuePair<Type, Type>> list)
        {
            foreach (var item in list)
            {
                containerBuilder.RegisterGeneric(item.Value).As(item.Key)
                    .PropertiesAutowired().InstancePerLifetimeScope();
            }
        }

        #endregion
    }
}
