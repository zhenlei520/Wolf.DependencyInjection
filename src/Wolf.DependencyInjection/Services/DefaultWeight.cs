// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Services;

/// <summary>
/// 默认权重
/// </summary>
public class DefaultWeight : IWeight
{
    /// <summary>
    /// 得到权重
    /// </summary>
    /// <returns></returns>
    public virtual int Weights => 99;
}
