// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using Wolf.DependencyInjection.Abstracts.Service;

namespace Wolf.DependencyInjection.Service
{
    /// <summary>
    /// 默认唯一标示
    /// </summary>
    public class DefaultIdentify : DefaultWeight, IIdentify
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public string Identify
        {
            get
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                return method.ReflectedType.Namespace;
            }
        }
    }
}
