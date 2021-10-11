// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection;

public static class ServiceCollectionExtensions
{
    #region 自动注入

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="packageNamePrefix">多个包前缀注入</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection serviceCollection,
        string[] packageNamePrefix)
    {
        string[] customPackageNamePrefix =
            packageNamePrefix.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        if (packageNamePrefix == null || packageNamePrefix.Length == 0 ||
            packageNamePrefix.All(string.IsNullOrWhiteSpace))
        {
            customPackageNamePrefix = new[] { "" };
        }

        var assemblies = customPackageNamePrefix.SelectMany(AssemblyCommon.GetSpecialAssemblies).ToArray();
        return serviceCollection.AddAutoInject(assemblies);
    }

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies">当前程序集需要用到注入的应用程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection services)
        => services.AddAutoInject(AppDomain.CurrentDomain.GetAssemblies());

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies">当前程序集需要用到注入的应用程序集</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (services.Any(s => s.ImplementationType == typeof(DependencyInjection)))
        {
            return services;
        }

        services.AddSingleton<DependencyInjection>();
        return new AutoRegister(services, assemblies).Build();
    }

    #endregion

    #region private methods

    private class DependencyInjection
    {
    }

    #endregion
}
