// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts;

public enum TypeCategory
{
    /// <summary>
    /// class and generic class and Interface and generic interface
    /// </summary>
    All = 1,

    /// <summary>
    /// 普通类
    /// </summary>
    Class,

    /// <summary>
    /// 泛型类
    /// 例如：List<string>
    /// </summary>
    GenericClass,

    /// <summary>
    /// 普通接口
    /// </summary>
    Interface,

    /// <summary>
    /// 泛型接口
    /// 例如：IList<string>
    /// </summary>
    GenericInterface

}
