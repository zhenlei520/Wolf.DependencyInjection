// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection;

/// <summary>
///
/// </summary>
public static class ServiceCollectionExtensions
{
    #region 自动注入

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="packageNamePrefix">多个包前缀注入</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection serviceCollection, params string[] packageNamePrefix)
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
    /// <param name="services"></param>
    /// <param name="assemblies">当前程序集需要用到注入的应用程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection services, params Assembly[] assemblies)
    {
        //if (services.Any(service => service.ServiceType == typeof(IDependencyInjection)))
        //{
        //    return services;
        //}
        //services.AddSingleton<IDependencyInjection>();
        services.AddAssembly(assemblies);
        return new AutoRegister(assemblies ?? throw new ArgumentNullException(nameof(assemblies))).Build(services);
    }

    #endregion

    #region private methods

    internal interface IDependencyInjection
    {

    }

    private static IServiceCollection AddAssembly(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
        if (assemblies.Length == 0) throw new ArgumentException($"{nameof(assemblies)} must be greater than 0");

        return services.AddGeneric<Assembly, IAssemblyCollection, AssemblyCollection>(assemblies);
    }

    private static IServiceCollection AddGeneric<T, TCollection, TCollectionImplementation>(this IServiceCollection services, params T[] array)
        where TCollection : class, IList<T>
        where TCollectionImplementation : class, TCollection
    {
        var implementationType = typeof(TCollectionImplementation);
        var collection = (TCollectionImplementation)Assembly.GetAssembly(implementationType).CreateInstance(implementationType.ToString());
        foreach (var item in array)
        {
            services.AddGeneric(collection, item);
        }
        services.AddSingleton(typeof(TCollection), collection);
        return services;
    }

    private static IServiceCollection AddGeneric<T>(this IServiceCollection services, IList<T> list, T item)
    {
        list.Add(item);
        return services;
    }

    #endregion
}
