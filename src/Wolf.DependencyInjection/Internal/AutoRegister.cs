namespace Wolf.DependencyInjection.Internal;

internal class AutoRegister
{
    private IAssemblyCollection _assemblyCollection;

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public virtual IServiceCollection Build(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        _assemblyCollection = serviceProvider.GetRequiredService<IAssemblyCollection>();

        #region 单例

        RegisterGeneric(services, typeof(ISingleInstance), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        RegisterGeneric(services, typeof(ISingleInstance<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);

        #endregion

        #region 请求

        RegisterGeneric(services, typeof(IPerRequest), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        RegisterGeneric(services, typeof(IPerRequest<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);

        #endregion

        #region 瞬时

        RegisterGeneric(services, typeof(IDependency), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        RegisterGeneric(services, typeof(IDependency<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);

        #endregion

        #region 注入并执行
        var serviceTypes = _assemblyCollection.GetTypes(typeof(IService), TypeCategory.Class);
        foreach (var serviceType in serviceTypes)
        {
            serviceProvider.GetService(serviceType);
        }
        #endregion

        return services;
    }

    #region 添加注入

    /// <summary>
    /// 添加注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="type"></param>
    /// <param name="serviceLifetime"></param>
    private void RegisterGeneric(IServiceCollection services, Type type, ServiceLifetime serviceLifetime)
    {
        var list = AssemblyCommon.GetInterfaceAndImplementationType(type, _assemblyCollection);
        foreach (var item in list)
        {
            services.TryAddEnumerable(new ServiceDescriptor(item.serviceType, item.implementationType, serviceLifetime));
        }
    }

    #endregion
}
