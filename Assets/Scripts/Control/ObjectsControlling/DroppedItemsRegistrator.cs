using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DroppedItemsRegistrator : MonoBehaviour 
{
    public List<DroppedItem> DroppedItems { get; private set; }

    private ObjectRegistrator _objectRegistrator;

    [Inject]
    private void Construct(ObjectRegistrator objectRegistrator)
    {
        _objectRegistrator = objectRegistrator;
    }

    public void Initialize()
    {
        DroppedItems = new();
        
        foreach (GameObject obj in _objectRegistrator.Enumerate())
        {
            if (obj.TryGetComponent(out DroppedItem item))
            {
                DroppedItems.Add(item);
            }
        }
    }
}
