using System.Collections.Generic;
using UnityEngine;

public class DescriptablesRegistrator : MonoBehaviour
{
    public List<IDescriptable> Descriptables { get; private set; }

    public void Initialize()
    {
        Descriptables = new();

        foreach (GameObject obj in ObjectRegistrator.Enumerate())
        {
            if (obj.TryGetComponent(out IDescriptable descriptable))
            {
                Descriptables.Add(descriptable);
            }
        }
    }
}
