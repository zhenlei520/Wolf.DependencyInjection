// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts.Service
{
    /// <summary>
    /// 唯一标示
    /// </summary>
    public interface IIdentify : IWeight
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        /// <returns></returns>
        string ServiceName { get; }
    }
}
