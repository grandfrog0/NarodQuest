using UnityEngine;
using Zenject;

public class OutlineInstaller : MonoInstaller
{
    [SerializeField] private OutlineController _outlineController;
    [SerializeField] private OutlineView _outlineViewPrefab;
    [SerializeField] private OutlineConfig _defaultOutlineConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_outlineController);

        Container.BindFactory<OutlineConfig, Transform, OutlineView, OutlineView.Factory>()
            .FromPoolableMemoryPool(x => x
                .FromComponentInNewPrefab(_outlineViewPrefab)
                );

        Container.BindInstance(_defaultOutlineConfig).WhenInjectedInto<OutlineController>();
    }
}
