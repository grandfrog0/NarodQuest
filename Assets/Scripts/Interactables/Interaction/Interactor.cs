using System;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private InteractablesSelector _objectSelector;
    [SerializeField] private OutlineController _outlineController; // TODO
    [SerializeField] private JoystickController _interactiveController;
    [SerializeField] private GameObject _interactionPointViewer;
    [SerializeField] private float _selectionDistanceMultiplier = 3f;
    private InventoryConfig _inventory;
    private BringableObjectController _bringableObjectController;

    public InteractableObject Target { get; private set; }
    private Vector3 _interactionPoint;

    public void Initialize(InteractablesSelector objectSelector, InventoryConfig inventory, BringableObjectController bringableController)
    {
        _objectSelector = objectSelector;
        _inventory = inventory;
        _bringableObjectController = bringableController;

        // TODO
        //if (_bringableObjectController.Current?.TryGetComponent(out TakeableObject takeableObject) == true)
        //{
        //    AddTarget(takeableObject);
        //}

        _interactiveController.OnDragging += OnPointerMove;
        _interactiveController.OnDragStart += OnPointerDragStart;
        _interactiveController.OnCancelled += OnPointerCancelled;
        _interactiveController.OnPerformed += OnPointerPerformed;
        _interactiveController.OnClick += OnPointerClick;
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
        _interactionPoint = transform.position + (Vector3)direction * _selectionDistanceMultiplier;

        _interactionPointViewer.transform.position = _interactionPoint;
        HandleNearest();
    }

    /// <summary>
    /// Джойстик отпущен - взаимодействуем, а затем скрываем область выбора
    /// </summary>
    public void OnPointerPerformed()
    {
        TryInteract();
        _interactionPointViewer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Джойстик просто был нажат - обновляем текущую цель и взаимодействуем
    /// </summary>
    public void OnPointerClick()
    {
        _interactionPoint = transform.position;
        HandleNearest();

        TryInteract();
        _interactionPointViewer.gameObject.SetActive(false);
    }

    /// <summary>
    /// Действие отменено - скрываем область выбора
    /// </summary>
    private void OnPointerCancelled()
    {
        _interactionPointViewer.gameObject.SetActive(false);
    }

    private void HandleNearest()
    {
        if (_bringableObjectController.Current != null)
        {
            Target = null;
            return;
        }
        Target = _objectSelector.Targets.OrderBy(x => Vector2.Distance(_interactionPoint, x.transform.position)).FirstOrDefault();
    }

    private bool TryInteract()
    {
        if (_bringableObjectController.Current != null)
        {
            _bringableObjectController.Current.Drop(_interactionPoint);
            return true;
        }

        if (Target != null)
        {
            Target.Interact(new InteractionData
            {
                Point = _interactionPoint,
                Player = gameObject,
                Inventory = _inventory,
                BringableController = _bringableObjectController
            });

            return true;
        }

        return false;
    }
}
