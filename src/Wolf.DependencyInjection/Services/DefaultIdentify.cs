// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Services;

/// <summary>
/// 默认唯一标示
/// </summary>
public class DefaultIdentify : DefaultWeight, IIdentify
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public virtual string ServiceName
    {
        get
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }
    }
}
