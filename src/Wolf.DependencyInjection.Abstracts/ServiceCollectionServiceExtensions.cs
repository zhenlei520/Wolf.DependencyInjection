// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.



namespace Wolf.DependencyInjection.Abstracts;

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection AddAssembly(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
        if (assemblies.Length == 0) throw new ArgumentException($"{nameof(assemblies)} must be greater than 0");

        return services.AddGeneric<Assembly, IAssemblyCollection, AssemblyCollection>(assemblies);
    }

    public static IServiceCollection AddGeneric<T, TCollection, TCollectionImplementation>(this IServiceCollection services, params T[] array)
        where TCollection : class, IList<T>
        where TCollectionImplementation : class, TCollection
    {
        if (services.All(service => service.ServiceType != typeof(T)))
        {
            services.AddSingleton<TCollection, TCollectionImplementation>();
        }
        var serviceProvider = services.BuildServiceProvider();
        var list = serviceProvider.GetRequiredService<TCollection>();
        foreach (var item in array)
        {
            services.AddGeneric(list, item);
        }

        return services;
    }

    private static IServiceCollection AddGeneric<T>(this IServiceCollection services, IList<T> list, T item)
    {
        list.Add(item);
        return services;
    }
}
