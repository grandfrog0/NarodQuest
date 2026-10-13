using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LocatorManager _locatorManager;
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private ObjectRegistrator _objectRegistrator;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private InventoryVisualizer _inventoryVisualizer;

    private void Start()
    {
        _objectRegistrator.Initialize();

        _inventoryManager.Initialize(new InventoryConfig());
        _inventoryVisualizer.Initialize();

        _locatorManager.Initialize();
        _transitionManager.Initialize();
    }
}
