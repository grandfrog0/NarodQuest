using System.Collections.Generic;
using UnityEngine;

public class DroppedItemsRegistrator : MonoBehaviour
{
    public List<DroppedItem> DroppedItems { get; private set; }

    public void Initialize()
    {
        DroppedItems = new();
        
        foreach (GameObject obj in ObjectRegistrator.Enumerate())
        {
            if (obj.TryGetComponent(out DroppedItem item))
            {
                DroppedItems.Add(item);
            }
        }
    }
}
