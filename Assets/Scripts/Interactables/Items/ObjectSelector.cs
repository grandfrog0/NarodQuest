using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private GameObject _selectionOutlinePrefab;
    [SerializeField] private LayerMask _selectMask;
    private Dictionary<InteractableObject, (GameObject, UnityAction<bool>)> _targets = new();
    private CircleCollider2D _collider;

    private void Start()
    {
        InputSystemManager.OnTouchAtPosition += OnScreenTouched;
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnScreenTouched(Vector2 screenPos)
    {
        if (_targets.Count == 0 || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screenPos), Vector3.zero, 1, _selectMask);

        if (hit.collider != null)
        {
            InteractableObject target = _targets.FirstOrDefault(x => x.Key.gameObject == hit.collider.gameObject).Key;
            if (target)
            {
                target.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (!_targets.Any(x => x.Key.gameObject == collision.gameObject))
            {
                InteractableObject target = collision.GetComponent<InteractableObject>();
                AddTarget(target);
            }
        }
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
        GameObject outline = Instantiate(_selectionOutlinePrefab, target.transform.position, Quaternion.identity);
        outline.transform.localScale = target.transform.localScale + Vector3.one;
        _targets.Add(target, (outline, action));
    }

    private void RemoveTarget(InteractableObject target)
    {
        _targets.Remove(target, out (GameObject target, UnityAction<bool> action) value);
        target.OnActiveChanged.RemoveListener(value.action);
        Destroy(value.target);
    }
}
