// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Internal;

internal class AutoRegister
{
    private List<ServiceDescriptor> _awaitServices = new();
    private List<(Type ImplementationType,ServiceLifetime ServiceLifetime)> _awaitImplementationServices = new();

    private readonly IServiceCollection _services;

    private readonly ServiceOptions _serviceOptions;

    public AutoRegister(IServiceCollection services, params Assembly[] assemblies)
    {
        _services = services;
        _serviceOptions = new ServiceOptions { Assemblies = assemblies };

        #region 单例

        AddAwaitServices(typeof(ISingleInstance), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);
        AddAwaitServices(typeof(ISingleInstance<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Singleton);

        #endregion

        #region 请求

        AddAwaitServices(typeof(IPerRequest), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);
        AddAwaitServices(typeof(IPerRequest<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Scoped);

        #endregion

        #region 瞬时

        AddAwaitServices(typeof(IDependency), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);
        AddAwaitServices(typeof(IDependency<,,,,,,,,,,,,,,,,,>), ServiceLifetime.Transient);

        #endregion
    }

    public virtual IServiceCollection Build()
    {
        AddServices();
        return _services;
    }

    #region private methods

    #region 添加到待添加的服务集合

    /// <summary>
    /// 添加到待添加的服务集合
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceLifetime"></param>
    private void AddAwaitServices(Type type, ServiceLifetime serviceLifetime)
    {
        foreach (var item in GetInterfaceAndImplementationType(type))
        {
            _awaitServices.Add(new ServiceDescriptor(item.ServiceType, item.ImplementationType, serviceLifetime));
        }
        foreach (var implementationType in GetImplementationType(type))
        {
            _awaitImplementationServices.Add((implementationType,serviceLifetime));
        }
    }

    #endregion

    #region 添加服务并触发

    /// <summary>
    /// 添加服务并触发
    /// </summary>
    private void AddServices()
    {
        foreach (var service in _awaitServices)
        {
            _services.TryAddEnumerable(service);
        }

        foreach (var service in _awaitImplementationServices)
        {
            switch (service.ServiceLifetime)
            {
                case ServiceLifetime.Singleton:
                    _services.TryAddSingleton(service.ImplementationType);
                    break;
                case ServiceLifetime.Scoped:
                    _services.TryAddScoped(service.ImplementationType);
                    break;
                case ServiceLifetime.Transient:
                    _services.AddTransient(service.ImplementationType);
                    break;
            }
        }

        var serviceProvider = _services.BuildServiceProvider();
        foreach (var service in _awaitServices)
        {
            if (typeof(IAutoFireService).IsAssignableFrom(service.ServiceType))
            {
                serviceProvider.GetRequiredService(service.ServiceType); //如果实现了自动触发
            }
        }
    }

    #endregion

    #region 查询继承type的接口与实现类集合

    /// <summary>
    /// 查询继承type的接口与实现类集合
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<(Type ServiceType, Type ImplementationType)> GetInterfaceAndImplementationType(Type type)
    {
        var classTypes = _serviceOptions.GetTypes(TypeCategory.Class, type);
        var interfaceTypes = _serviceOptions.GetTypes(TypeCategory.Interface, type);

        var list = new List<(Type ServiceType, Type ImplementationType)>();

        foreach (var interfaceType in interfaceTypes)
        {
            foreach (var classType in classTypes)
            {
                if (interfaceType.IsAssignableFrom(classType) &&
                    classType.GetInterfaces().Count(_ => typeof(IOnceService).IsAssignableFrom(_)) <= 2)
                {
                    list.Add((interfaceType, classType));
                }
            }
        }

        return list;
    }

    /// <summary>
    /// 查询继承type的接口与实现类集合
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private List<Type> GetImplementationType(Type type)
    {
        var classTypes = _serviceOptions.GetTypes(TypeCategory.Class, type);
        var implementationTypes = new List<Type>();
        foreach (var classType in classTypes)
        {
            if (classType.GetInterfaces().Count(type.IsAssignableFrom) == 1
                && classType.GetInterfaces().Count(_ => typeof(IOnceService).IsAssignableFrom(_)) <= 1
                && IsAdd(classType))
            {
                implementationTypes.Add(classType);
            }
        }

        return implementationTypes;

        bool IsAdd(Type classType)
        {
            if (classType.BaseType == null)
            {
                return true;
            }

            if (type.IsAssignableFrom(classType.BaseType) && typeof(IOnceService).IsAssignableFrom(classType.BaseType))
            {
                return false;
            }

            return IsAdd(classType.BaseType);
        }
    }

    #endregion

    #endregion
}
