using UnityEngine;
using Zenject;

public class LocatingInstaller : MonoInstaller
{
    [SerializeField] private LocationsContainer _locationsContainer;
    [SerializeField] private PlayerLocationHandler _playerLocationHandler;
    [SerializeField] private Location _defaultLocation;
    [SerializeField] private LocatorManager _locatorManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_locationsContainer);
        Container.BindInstance(_playerLocationHandler);
        Container.BindInstance(_defaultLocation);

        Container.BindInstance(_locatorManager);

        Container.Bind<LocatorPositionCalculator>().AsSingle();
    }
}