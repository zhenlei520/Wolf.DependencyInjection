﻿// Copyright (c) zhenlei520 All rights reserved.

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
        /// <param name="packageNamePrefix">多个包前缀注入</param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            params string[] packageNamePrefix)
        {
            return serviceCollection.Build(null, packageNamePrefix);
        }

        /// <summary>
        /// 得到ServiceProvider
        ///
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="action"></param>
        /// <param name="packageNamePrefix">多个包前缀注入</param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection,
            Action<ContainerBuilder> action = null, params string[] packageNamePrefix)
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