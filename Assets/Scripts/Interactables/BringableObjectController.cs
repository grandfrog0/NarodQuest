using UnityEngine;

public class BringableObjectController : MonoBehaviour
{
    public static BringableObjectController Instance { get; private set; }

    public PlayerBringable Current { get; private set; }
    [SerializeField] private Vector2 _offset = Vector2.down;
    private float _radius = 0.5f;
    private PlayerMovementController _movementController;

    public bool SwitchBring(PlayerBringable bringable)
    {
        if (Current != bringable)
        {
            Bring(bringable);
            return true;
        }
        else
        {
            Detach();
            return false;
        }
    }
    public void Bring(PlayerBringable bringable)
    {
        Detach();

        Current = bringable;

        Current.transform.parent = transform;
        OnMoveAxisChanged(_movementController.Axis);
        _movementController.OnMoveAxisChanged.AddListener(OnMoveAxisChanged);
    }

    public void Detach()
    {
        if (Current == null)
        {
            return;
        }

        Current.Drop();
        _movementController.OnMoveAxisChanged.RemoveListener(OnMoveAxisChanged);
        Current = null;
    }

    private void OnMoveAxisChanged(Vector2 axis)
    {
        if (!Current)
        {
            return;
        }

        Current.transform.localPosition = _offset + axis * _radius;
    }

    private void Start()
    {
        _movementController = GetComponent<PlayerMovementController>();
        Instance = this;
    }

    private void OnDestroy()
    {
        Detach();
    }
}
