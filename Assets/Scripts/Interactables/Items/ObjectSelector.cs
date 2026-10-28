using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private OutlineView _selectionOutlinePrefab;
    [SerializeField] private LayerMask _selectMask;
    [SerializeField] private OutlineConfig _defaultOutlineConfig;
    private Dictionary<InteractableObject, (OutlineView, UnityAction<bool>)> _targets = new();
    private CircleCollider2D _collider;

    private InventoryConfig _inventory;
    private BringableObjectController _bringableObjectController;

    public void Initialize(InventoryConfig inventory, BringableObjectController bringableController)
    {
        _collider = GetComponent<CircleCollider2D>();
        InputSystemManager.OnTouchAtPosition.AddListener(OnScreenTouched);

        _inventory = inventory;
        _bringableObjectController = bringableController;

        if (_bringableObjectController.Current?.TryGetComponent(out TakeableObject takeableObject) == true)
        {
            AddTarget(takeableObject);
        }

        //CheckTargets();
    }

    private void OnScreenTouched(Vector2 screenPos)
    {
        if (_targets.Count == 0 || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(screenPos), Vector3.zero, 1, _selectMask);

        if (hits.Length > 0)
        {
            InteractableObject target = _targets.OrderByDescending(x => x.Key.SelectionOrder).FirstOrDefault(x => hits.Select(x => x.collider.gameObject).Contains(x.Key.gameObject)).Key;
            if (target)
            {
                target.Interact(new InteractionData
                {
                    Point = hits.FirstOrDefault(x => x.collider.gameObject == target.gameObject).point,
                    Player = gameObject,
                    Inventory = _inventory,
                    BringableController = _bringableObjectController
                });
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ValidateTarget(collision);
    }

    //private void CheckTargets()
    //{
    //    Collider2D[] colliders = new Collider2D[9];
    //    int c = Physics2D.OverlapCircle((Vector2)transform.position + _collider.offset, _collider.radius, ContactFilter2D.noFilter, colliders);
    //        Debug.Log(c);
    //    for (int i = 0; i < c; i++)
    //    {
    //        Debug.Log((colliders[i], ValidateTarget(colliders[i])));
    //    }
    //}

    private bool ValidateTarget(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (!_targets.Any(x => x.Key.gameObject == collision.gameObject))
            {
                InteractableObject target = collision.GetComponent<InteractableObject>();
                if (target.IsActive)
                {
                    AddTarget(target);
                    return true;
                }
            }
        }

        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") && Vector3.Distance(transform.position, collision.transform.position) > _collider.radius)
        {
            if (_targets.Any(x => x.Key.gameObject == collision.gameObject))
            {
                InteractableObject target = collision.GetComponent<InteractableObject>();
                RemoveTarget(target);
            }
        }
    }

    private void OnActiveChanged(InteractableObject target, bool value)
    {
        if (!value)
        {
            RemoveTarget(target);
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= _collider.radius)
            {
                AddTarget(target);
            }
        }
    }

    private void AddTarget(InteractableObject target)
    {
        if (_targets.ContainsKey(target))
        {
            return;
        }

        UnityAction<bool> action = x => OnActiveChanged(target, x);
        target.OnActiveChanged.AddListener(action);

        OutlineView outline = Instantiate(_selectionOutlinePrefab, target.transform);
        outline.Initalize(target.Outline ?? _defaultOutlineConfig);
        _targets.Add(target, (outline, action));
    }

    private void RemoveTarget(InteractableObject target)
    {
        _targets.Remove(target, out (OutlineView target, UnityAction<bool> action) value);
        target.OnActiveChanged.RemoveListener(value.action);
        Destroy(value.target.gameObject);
    }
}
