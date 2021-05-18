// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Wolf.Systems.Core;

namespace Wolf.DependencyInjection.Internal
{
    /// <summary>
    ///
    /// </summary>
    internal class AssemblyCommon
    {
        #region 获取类库名称以**开头的应用程序集

        /// <summary>
        ///
        /// </summary>
        /// <param name="packageNamePre">包名前缀，若为空，则返回所有包信息</param>
        /// <returns></returns>
        public static Assembly[] GetSpecialAssemblies(string packageNamePre)
        {
            List<Assembly> list = new List<Assembly>();
            var deps = DependencyContext.Default;
            Expression<Func<CompilationLibrary, bool>> condition = x => true;
            if (!string.IsNullOrEmpty(packageNamePre))
            {
                condition = condition.And(lib =>
                    lib.Name.IndexOf(packageNamePre, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            List<CompilationLibrary> libs = deps.CompileLibraries
                .Where(condition.Compile()).ToList();
            foreach (var lib in libs)
            {
                try
                {
                    list.Add(Assembly.Load(lib.Name));
                }
#pragma warning disable 168
                catch (Exception ex)
#pragma warning restore 168
                {
                    // ignored
                }
            }

            return list.ToArray();
        }

        #endregion
    }
}
