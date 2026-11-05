using UnityEngine;
using UnityEngine.Events;

public class BringableObjectController : MonoBehaviour
{
    public static BringableObjectController Instance { get; private set; }

    public bool HasObject => Current != null;
    public PlayerBringable Current { get; private set; }
    [SerializeField] private Vector2 _offset = Vector2.down;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _obstacleMask;
    private PlayerMovementController _movementController;
    private Vector2 _lastAxis;

    public void Initialize()
    {
        _movementController = GetComponent<PlayerMovementController>();
        Instance = this;
    }

    public bool IsCorrectPlace(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 1f, _obstacleMask);
        return hit.collider == null;
    }

    public bool SwitchBring(PlayerBringable bringable, Vector3 point)
    {
        if (Current != bringable)
        {
            Bring(bringable);
            return true;
        }
        else
        {
            TryDetach(point);
            return false;
        }
    }

    public void Bring(PlayerBringable bringable)
    {
        TryDetach();

        Current = bringable;

        Current.Drag(transform);
        OnMoveAxisChanged(_movementController.Axis);
        _movementController.OnMoveAxisChanged.AddListener(OnMoveAxisChanged);
    }

    public bool TryDetach(Vector3 point = default, bool savePosition = false)
    {
        if (Current == null || !IsCorrectPlace(point))
        {
            return false;
        }

        if (savePosition)
        {
            point = (Vector2)transform.position + _offset/* + Current.GetOffset(_lastAxis)*/ + _lastAxis * _radius;
        }
        Current.Drop(point);
        _movementController.OnMoveAxisChanged.RemoveListener(OnMoveAxisChanged);

        Current = null;

        return true;
    }

    private void OnMoveAxisChanged(Vector2 axis)
    {
        if (!Current)
        {
            return;
        }

        Current.transform.localPosition = _offset + Current.GetOffset(axis) + axis * _radius;
        Current.Renderer.SortingOrder = axis == Vector2.down ? 1 : 0;

        _lastAxis = axis;
    }

    private void OnDestroy()
    {
        TryDetach();
    }
}
