using UnityEngine;
using Zenject;

public class OutlineInstaller : MonoInstaller
{
    [SerializeField] private OutlineView _outlineViewPrefab;
    [SerializeField] private OutlineConfig _defaultOutlineConfig;

    public override void InstallBindings()
    {
        Container.Bind<OutlineController>().FromNew().AsSingle();

        Container.BindFactory<OutlineData, Transform, OutlineView, OutlineView.Factory>()
            .FromPoolableMemoryPool(x => x
                .FromComponentInNewPrefab(_outlineViewPrefab)
                );

        Container.BindInstance(_defaultOutlineConfig).WhenInjectedInto<OutlineController>();
    }
}
