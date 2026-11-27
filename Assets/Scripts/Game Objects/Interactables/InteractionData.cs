using UnityEngine;

public class InteractionData
{
    public Vector3 Point { get; }
    public GameObject Player { get; }
    public InventoryData Inventory { get; }
    public InventoryManager InventoryManager { get; }
    public BringableObjectController BringableController { get; }

    public InteractionData(
        Vector3 point,
        GameObject player,
        InventoryData inventory,
        InventoryManager inventoryManager,
        BringableObjectController bringableController
        )
    {
        Point = point;
        Player = player;
        Inventory = inventory;
        InventoryManager = inventoryManager;
        BringableController = bringableController;
    }
}
