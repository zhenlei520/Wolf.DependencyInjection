// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts;

public class ServiceBase : IService
{
    public IServiceCollection Services { get; protected set; }

    public ServiceBase(IServiceCollection services)
    {
        Services = services;
    }
}
