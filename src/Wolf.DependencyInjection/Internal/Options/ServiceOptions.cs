// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Internal.Options;

internal class ServiceOptions
{
    private Assembly[] _assemblies = new Assembly[0];

    public Assembly[] Assemblies
    {
        get => _assemblies;
        set
        {
            _assemblies = value;
            if (_assemblies == null || _assemblies.Length == 0)
            {
                throw new ArgumentNullException(nameof(_assemblies));
            }

            var allTypes = _assemblies.SelectMany(assembly => assembly.GetTypes()).ToList();
            AllClassTypes = allTypes.Where(type => type.IsClass && !type.IsGeneric() && !type.IsAbstract).ToArray();
            AllInterfaceTypes = allTypes.Where(type => type.IsInterface && !type.IsGeneric()).ToArray();
        }
    }

    private Type[] AllClassTypes { get; set; }

    private Type[] AllInterfaceTypes { get; set; }

    public IEnumerable<Type> GetTypes(TypeCategory category, Type serviceType)
    {
        if (category == TypeCategory.Class)
        {
            return serviceType.GetTypes(AllClassTypes.ToArray());
        }

        if (category == TypeCategory.Interface)
        {
            return serviceType.GetTypes(AllInterfaceTypes.ToArray()).Where(type => type != serviceType);
        }

        throw new NotSupportedException(nameof(category));
    }
}
