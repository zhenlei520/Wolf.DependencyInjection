// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Wolf.DependencyInjection.Abstracts;

public class AssemblyCollection : IAssemblyCollection
{
    private readonly List<Assembly> _assemblies = new();

    private readonly List<Type> _types = new();

    public Assembly this[int index]
    {
        get
        {
            return _assemblies[index];
        }
        set
        {
            _assemblies[index] = value;
        }
    }

    public int Count => _assemblies.Count;

    public bool IsReadOnly => false;

    public void Add(Assembly item)
    {
        _assemblies.Add(item);
        _types.AddRange(item.GetTypes());
    }

    public void Clear()
    {
        _assemblies.Clear();
        _types.Clear();
    }

    public bool Contains(Assembly item) => _assemblies.Contains(item);

    public void CopyTo(Assembly[] array, int arrayIndex)
    {
        _assemblies.CopyTo(array, arrayIndex);
        _types.AddRange(array.SelectMany(assembly => assembly.GetTypes()));
    }

    public IEnumerator<Assembly> GetEnumerator() => _assemblies.GetEnumerator();

    public List<Type> GetTypes() => _types;

    public List<Type> GetTypes(Type type, TypeCategory category = TypeCategory.All)
    {
        var list = new List<Type>();

        return list;
    }

    public int IndexOf(Assembly item) => _assemblies.IndexOf(item);

    public void Insert(int index, Assembly item)
    {
        _assemblies.Insert(index, item);
        _types.AddRange(item.GetTypes());
    }

    public bool Remove(Assembly item)
    {
        var res = _assemblies.Remove(item);
        if (res)
        {
            foreach (var type in item.GetTypes())
            {
                _types.Remove(type);
            }
        }
        return res;
    }

    public void RemoveAt(int index)
    {
        var assembly = _assemblies[index];
        foreach (var type in assembly.GetTypes())
        {
            _types.Remove(type);
        }
        _assemblies.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
