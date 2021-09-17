// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts;

public interface IAssemblyCollection : IList<Assembly>
{
    /// <summary>
    /// 得到当前应用程序集的Type集合
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTypes();

    /// <summary>
    /// 得到当前应用程序集下继承type的集合
    /// </summary>
    /// <param name="type"></param>
    /// <param name="category">类型：默认获取全部的类</param>
    /// <returns></returns>
    public List<Type> GetTypes(Type type, TypeCategory category = TypeCategory.All);
}
