using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DescriptablesRegistrator : MonoBehaviour
{
    public List<IDescriptable> Descriptables { get; private set; }

    private ObjectRegistrator _objectRegistrator;

    [Inject]
    private void Construct(ObjectRegistrator objectRegistrator)
    {
        _objectRegistrator = objectRegistrator;
    }

    public void Initialize()
    {
        Descriptables = new();

        foreach (GameObject obj in _objectRegistrator.Enumerate())
        {
            if (obj.TryGetComponent(out IDescriptable descriptable))
            {
                Descriptables.Add(descriptable);
            }
        }
    }
}
