using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Data")]
    [SerializeField] private GameObject _player;
    [SerializeField] private JoystickController _moveJoystick;
    [SerializeField] private JoystickController _interactJoystick;

    [Header("Main")]
    [SerializeField] private InputSystemManager _inputSystemManager;
    [SerializeField] private ObjectRegistrator _objectRegistrator;
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private StateSaver _stateSaver;
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private DialogWindowManager _dialogWindowManager;

    [Header("Registrators")]
    [SerializeField] private CameraConstraintsRegistrator _cameraConstraintsRegistrator;
    [SerializeField] private DroppedItemsRegistrator _droppedItemsRegistrator;
    [SerializeField] private DescriptablesRegistrator _descriptablesRegistrator;
    [SerializeField] private QuestsRegistrator _questsRegistrator;
    [SerializeField] private StateObjectsRegistrator _stateObjectsRegistrator;
    [SerializeField] private ItemRegistrator _itemRegistrator;


    [Header("Services")]
    [SerializeField] private CinemachineCameraController _cameraController;
    [SerializeField] private BringableObjectController _bringableObjectController;
    [SerializeField] private PlayerMovementController _playerMove;
    [SerializeField] private InteractablesSelector _interactablesSelector;
    [SerializeField] private Interactor _interactor;
    [SerializeField] private LocatorManager _locatorManager;

    public override void InstallBindings()
    {
        // Data
        Container.BindInstance(_player).When(x => x.MemberName == "player");
        Container.BindInstance(_moveJoystick).When(x => x.MemberName == "moveJoystick");
        Container.BindInstance(_interactJoystick).When(x => x.MemberName == "interactJoystick");

        // Main
        Container.BindInstance(_inputSystemManager);
        Container.BindInstance(_objectRegistrator);
        Container.BindInstance(_saveManager);
        Container.BindInstance(_stateSaver);
        Container.BindInstance(_transitionManager);
        Container.BindInstance(_dialogManager);
        Container.BindInstance(_dialogWindowManager);

        // Registrators
        Container.BindInstance(_cameraConstraintsRegistrator);
        Container.BindInstance(_droppedItemsRegistrator);
        Container.BindInstance(_descriptablesRegistrator);
        Container.BindInstance(_questsRegistrator);
        Container.BindInstance(_stateObjectsRegistrator);
        Container.BindInstance(_itemRegistrator);

        // Services
        Container.BindInstance(_cameraController);
        Container.BindInstance(_bringableObjectController);
        Container.BindInstance(_playerMove);
        Container.BindInstance(_interactablesSelector);
        Container.BindInstance(_interactor);
        Container.BindInstance(_locatorManager);
    }
}
