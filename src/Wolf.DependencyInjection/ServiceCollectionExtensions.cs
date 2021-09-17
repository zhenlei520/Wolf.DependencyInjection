// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ServiceCollectionExtensions
{
    #region 匹配接口没有实现时添加

    /// <summary>
    /// 匹配接口没有实现时添加
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

    #endregion

    #region 匹配接口与实现类不一致时添加，否则不添加

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
    /// 匹配接口与实现类不一致时添加，否则不添加
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

    #endregion

    #region 移除所有实现

    /// <summary>
    /// 移除所有实现
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <typeparam name="TService">接口</typeparam>
    /// <returns></returns>
    public static IServiceCollection RemoveAll<TService>(this IServiceCollection serviceCollection)
        where TService : class
    {
        return ServiceCollectionDescriptorExtensions.RemoveAll<TService>(serviceCollection);
    }

    #endregion

    #region 自动注入

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="packageNamePrefix">多个包前缀注入</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection serviceCollection,
        params string[] packageNamePrefix)
    {
        Assembly[] assemblies;
        if (packageNamePrefix == null || packageNamePrefix.Length == 0 ||
            packageNamePrefix.All(string.IsNullOrWhiteSpace))
        {
            assemblies = AssemblyCommon.GetSpecialAssemblies("");
        }
        else
        {
            assemblies = packageNamePrefix.Where(x => !string.IsNullOrWhiteSpace(x))
                .SelectMany(AssemblyCommon.GetSpecialAssemblies).ToArray();
        }

        return serviceCollection.AddAutoInject(assemblies);
    }

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="assembly">当前程序集需要用到注入的应用程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection serviceCollection, Assembly[] assembly)
    {
        return new AutoRegister(assembly).AddAutoInject(serviceCollection);
    }

    #endregion
}
