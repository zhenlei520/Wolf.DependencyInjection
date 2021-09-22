// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Wolf.DependencyInjection.Abstracts;

public enum TypeCategory
{
    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    All = 1,

    /// <summary>
    /// 普通类,不包含抽象类
    /// </summary>
    [Description("普通类")]
    Class,

    /// <summary>
    /// 泛型类
    /// 例如：List<string>
    /// </summary>
    [Description("泛型类")]
    GenericClass,

    /// <summary>
    /// 普通接口
    /// </summary>
    [Description("普通接口")]
    Interface,

    /// <summary>
    /// 泛型接口
    /// 例如：IList<string>
    /// </summary>
    [Description("泛型接口")]
    GenericInterface
}
