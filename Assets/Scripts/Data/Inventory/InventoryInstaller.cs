using Unity.Collections;
using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private InventoryVisualizer _inventoryVisualizer;

    public override void InstallBindings()
    {
        Container.BindInstance(_inventoryManager);
        Container.BindInstance(_inventoryVisualizer);

        Container.Bind<InventoryData>().FromNew().AsSingle();
    }
}
