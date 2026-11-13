using System.Linq;
using UnityEngine;
using Zenject;

public class Interactor : MonoBehaviour
{
    [SerializeField] private InteractionPointViewer _interactionPointViewer;
    [SerializeField] private float _selectionDistanceMultiplier = 3f;

    private JoystickController _interactJoystick;
    private InteractablesSelector _interactablesSelector;
    private OutlineController _outlineController;
    private BringableObjectController _bringableObjectController;

    private InventoryData _inventory;

    private InteractableObject _target;
    public InteractableObject Target
    {
        get => _target;
        private set
        {
            if (_target != value)
            {
                if (_target is not null)
                {
                    _outlineController.DeselectTarget(_target);
                }

                _target = value;

                if (_target is not null)
                {
                    _outlineController.SelectTarget(_target);
                }
            }
        }
    }
    private Vector3 _interactionPoint;

    [Inject]
    private void Construct(JoystickController interactJoystick, InteractablesSelector interactableSelector, OutlineController outlineController, BringableObjectController bringableController)
    {
        _interactJoystick = interactJoystick;
        _interactablesSelector = interactableSelector;
        _outlineController = outlineController;
        _bringableObjectController = bringableController;
    }

    public void Initialize(InventoryData inventory)
    {
        _inventory = inventory;

        _interactJoystick.OnDragging += OnPointerMove;
        _interactJoystick.OnDragStart += OnPointerDragStart;
        _interactJoystick.OnCancelled += OnPointerCancelled;
        _interactJoystick.OnPerformed += OnPointerPerformed;
        _interactJoystick.OnClick += OnPointerClick;
    }

    private void Update()
    {
        HandleNearest();
    }

    /// <summary>
    /// Джойстик схвачен, показываем область выбора объектов
    /// </summary>
    public void OnPointerDragStart()
    {
        _interactionPointViewer.gameObject.SetActive(true);
    }

    /// <summary>
    /// Джойстик двигается, меняем позицию области выбора и текущую цель
    /// </summary>
    /// <param name="direction"></param>
    public void OnPointerMove(Vector2 direction)
    {
        _interactionPoint = _interactablesSelector.transform.position + (Vector3)direction * _selectionDistanceMultiplier;

        _interactionPointViewer.transform.position = _interactionPoint;
    }

    /// <summary>
    /// Джойстик отпущен - взаимодействуем, а затем скрываем область выбора
    /// </summary>
    public void OnPointerPerformed()
    {
        TryInteract();

        _interactionPoint = _interactablesSelector.transform.position;
        _interactionPointViewer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Джойстик просто был нажат - обновляем текущую цель и взаимодействуем
    /// </summary>
    public void OnPointerClick()
    {
        _interactionPoint = _interactablesSelector.transform.position;

        TryInteract();
        _interactionPointViewer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Действие отменено - скрываем область выбора
    /// </summary>
    private void OnPointerCancelled()
    {
        Target = null;

        _interactionPoint = _interactablesSelector.transform.position;
        _interactionPointViewer.gameObject.SetActive(false);
    }

    private void HandleNearest()
    {
        Target = _interactablesSelector.Targets.OrderBy(x => Vector2.Distance(_interactionPoint, x.transform.position)).FirstOrDefault();

        if (_bringableObjectController.Current != null)
        {
            Target = null;
            _interactionPointViewer.SetPlaceCorrect(_bringableObjectController.IsCorrectPlace(_interactionPoint));
        }
    }

    private bool TryInteract()
    {
        if (_bringableObjectController.Current != null && Target is not Table)
        {
            bool result = _bringableObjectController.TryDetach(_interactionPoint, _interactionPoint == _interactablesSelector.transform.position);
            Target = null;
            return result;
        }

        if (Target != null)
        {
            _outlineController.OnBeforeTargetInteracting(Target);

            Target.Interact(new InteractionData
            {
                Point = _interactionPoint,
                Player = gameObject,
                Inventory = _inventory,
                BringableController = _bringableObjectController
            });

            if (Target.IsActive == false)
            {
                _interactablesSelector.RemoveTarget(Target);
            }

            Target = null;

            return true;
        }

        return false;
    }
}
