// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UUPT.Extensions.DependencyInjection.Abstracts.Common;
using UUPT.Extensions.DependencyInjection.Autofac.Extension;

namespace UUPT.Extensions.DependencyInjection.Autofac
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region 得到ServiceProvider

        /// <summary>
        /// 得到ServiceProvider
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="packageNamePre"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceProvider Build(this IServiceCollection serviceCollection, string packageNamePre = "UUPT",
            Action<ContainerBuilder> action = null)
        {
            return Build(serviceCollection, AssemblyCommon.GetSpecialAssemblies(packageNamePre), action);
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

        #endregion
    }
}