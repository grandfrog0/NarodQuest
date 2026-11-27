using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TaggedObjectsContainer
{
    private HashSet<TaggedObject> _taggedObjects;

    [Inject]
    public TaggedObjectsContainer(ObjectRegistrator registrator)
    {
        _taggedObjects = registrator.Enumerate().Select(x => x.GetComponent<TaggedObject>()).Where(x => x != null).ToHashSet();
        Debug.Log($"Tagged objects: {string.Join("; ", _taggedObjects)}");
    }

    public T Get<T>(string tag) where T : Component
    {
        return _taggedObjects.FirstOrDefault(x => x.Tag == tag)?.GetComponent<T>();
    }
}
