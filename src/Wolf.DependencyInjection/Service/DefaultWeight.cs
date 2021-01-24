// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Wolf.DependencyInjection.Abstracts.Service;

namespace Wolf.DependencyInjection.Service
{
    /// <summary>
    /// 默认权重
    /// </summary>
    public class DefaultWeight : IWeight
    {
        /// <summary>
        /// 得到权重
        /// </summary>
        /// <returns></returns>
        public int Weights => 99;
    }
}
