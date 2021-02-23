// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Wolf.DependencyInjection.Autofac.Extension;
using Wolf.DependencyInjection.Autofac.Internal;

namespace Wolf.DependencyInjection.Autofac
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <param name="packageNamePre">多个包前缀注入</param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            Action<ContainerBuilder> action = null, params string[] packageNamePre)
        {
            Assembly[] assemblies;
            if (packageNamePre == null || packageNamePre.Length == 0|| packageNamePre.All(string.IsNullOrWhiteSpace))
            {
                assemblies = AssemblyCommon.GetSpecialAssemblies("");
            }
            else
            {
                assemblies = packageNamePre.Where(x=>!string.IsNullOrWhiteSpace(x)).SelectMany(AssemblyCommon.GetSpecialAssemblies).ToArray();
            }

            return serviceCollection.Build(assemblies, action);
        }

        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="assembly">当前程序集需要用到注入的应用程序集</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection, Assembly[] assembly,
            Action<ContainerBuilder> action = null)
        {
            serviceCollection.AddAutofac(action);
            return new AutofacAutoRegister(assembly).Build(serviceCollection, action);
        }
    }
}
