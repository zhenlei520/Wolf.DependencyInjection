namespace Wolf.DependencyInjection.Internal;

internal class AutoRegister
{
    private readonly Assembly[] _assemblies;

    /// <summary>
    /// 不建议使用当前程序集域，因为依赖注入采用惰性加载，可能会导致所需要的应用程序集缺失
    /// </summary>
    public AutoRegister() : this(AppDomain.CurrentDomain.GetAssemblies())
    {
    }

    /// <summary>
    ///
    /// </summary>
    public AutoRegister(Assembly[] assemblies) => _assemblies = assemblies;

    /// <summary>
    ///
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public virtual IServiceCollection Build(IServiceCollection serviceCollection)
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
    /// <param name="services"></param>
    /// <param name="type"></param>
    /// <param name="serviceLifetime"></param>
    private void RegisterGeneric(IServiceCollection services, Type type, ServiceLifetime serviceLifetime)
    {
        var list = AssemblyCommon.GetInterfaceAndImplementationType(type, services.BuildServiceProvider().GetRequiredService<IAssemblyCollection>());
        foreach (var item in list)
        {
            services.TryAddEnumerable(new ServiceDescriptor(item.serviceType, item.implementationType, serviceLifetime));
        }
    }

    #endregion
}
