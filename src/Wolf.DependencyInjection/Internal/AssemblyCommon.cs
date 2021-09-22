// Copyright (c) zhenlei520 All rights reserved.

namespace Wolf.DependencyInjection.Internal;

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

    #region 查询继承type的接口与实现类集合

    /// <summary>
    /// 查询继承type的接口与实现类集合
    /// </summary>
    /// <param name="type"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static List<(Type serviceType, Type implementationType)> GetInterfaceAndImplementationType(Type type, IAssemblyCollection assemblies)
    {
        var classTypes = assemblies.GetTypes(type, TypeCategory.Class);
        var interfaceTypes = assemblies.GetTypes(type, TypeCategory.Interface);

        var list = new List<(Type serviceType, Type implementationType)>();

        foreach (var interfaceType in interfaceTypes)
        {
            foreach (var classType in classTypes)
            {
                if (interfaceType.IsAssignableFrom(classType))
                {
                    list.Add((interfaceType, classType));
                }
            }
        }

        return list;
    }

    #endregion
}
