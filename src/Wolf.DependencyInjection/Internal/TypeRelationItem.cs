// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Wolf.DependencyInjection.Internal;

internal class TypeRelationItem
{
    public TypeRelationItem(TypeCategory category, IEnumerable<Type> types)
    {
        Category = category;
        Types = types.ToList();
    }

    public TypeCategory Category { get; set; }

    public List<Type> Types { get; set; }

    public static List<TypeRelationItem> GetRelationItems(List<Type> types, Type serviceType)
    {
        List<TypeRelationItem> relations = new();
        foreach (var item in typeof(TypeCategory).ToEnumAndAttributes<DescriptionAttribute>())
        {
            relations.Add(new TypeRelationItem((TypeCategory)item.Key, GetTypes(types, serviceType, (TypeCategory)item.Key)));
        }
        return relations;
    }

    private static IEnumerable<Type> GetTypes(List<Type> types, Type serviceType, TypeCategory category)
    {
        var typeList = serviceType.GetTypes(types.ToArray());
        if (category == TypeCategory.Class)
        {
            return typeList.Where(type => type.IsClass && !type.IsGeneric() && !type.IsAbstract).ToList();
        }
        else if (category == TypeCategory.GenericClass)
        {
            return typeList.Where(type => type.IsGenericClass()).ToList();
        }
        else if (category == TypeCategory.Interface)
        {
            return typeList.Where(type => type.IsInterface && !type.IsGeneric()).ToList();
        }
        else if (category == TypeCategory.GenericInterface)
        {
            return typeList.Where(type => type.IsGenericInterface()).ToList();
        }
        return typeList;
    }
}
