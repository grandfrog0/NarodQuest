using Unity.Collections;
using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] private InventoryVisualizer _inventoryVisualizer;

    public override void InstallBindings()
    {
        Container.Bind<InventoryManager>().AsSingle();
        Container.Bind<InventoryData>().AsSingle();

        Container.BindInstance(_inventoryVisualizer);
    }
}
