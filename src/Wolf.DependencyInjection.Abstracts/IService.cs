// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts;

/// <summary>
/// 继承此接口时会自动注入并自动触发
/// </summary>
public interface IService
{
    IServiceCollection Services { get; }
}
