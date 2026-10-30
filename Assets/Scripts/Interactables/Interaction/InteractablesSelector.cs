using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractablesSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _selectMask;
    private CircleCollider2D _collider;

    private HashSet<InteractableObject> _interactables;
    private Dictionary<InteractableObject, UnityAction<bool>> _activeListeners;

    public IEnumerable<InteractableObject> Targets => _interactables;

    public void Initialize()
    {
        _collider = GetComponent<CircleCollider2D>();
        _interactables = new();
        _activeListeners = new();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (!_interactables.Any(x => x.gameObject == collision.gameObject))
            {
                InteractableObject target = collision.GetComponent<InteractableObject>();
                if (target.IsActive)
                {
                    AddTarget(target);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") && Vector3.Distance(transform.position, collision.transform.position) > _collider.radius)
        {
            if (_interactables.Any(x => x.gameObject == collision.gameObject))
            {
                InteractableObject target = collision.GetComponent<InteractableObject>();
                RemoveTarget(target);
            }
        }
    }

    private void AddTarget(InteractableObject target)
    {
        if (_interactables.Contains(target))
        {
            return;
        }

        UnityAction<bool> activeListener = x => OnActiveChanged(target, x);

        _activeListeners[target] = activeListener;
        target.OnActiveChanged.AddListener(activeListener);

        _interactables.Add(target);
    }

    private void RemoveTarget(InteractableObject target)
    {
        if (!_interactables.Contains(target))
        {
            return;
        }

        target.OnActiveChanged.RemoveListener(_activeListeners[target]);
        _activeListeners.Remove(target);

        _interactables.Remove(target);
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
}
