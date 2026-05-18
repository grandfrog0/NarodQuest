using UnityEngine;

public class MoveWithPlayerObject : MonoBehaviour
{
    [SerializeField] private float _offset = 0.75f;
    [SerializeField] private bool _subscribeOnStart = false;
    private Transform _defaultParent;
    public PlayerMovementController Owner { get; private set; }

    private void Start()
    {
        _defaultParent = transform.parent;

        if (_subscribeOnStart)
        {
            Subscribe();
        }
    }

    public void Subscribe()
    {
        Owner = PlayerMovementController.Instance;
        transform.SetParent(Owner.transform);
        OnMovementAxisChanged(Owner.Axis);
        Owner.OnMovementAxisChanged.AddListener(OnMovementAxisChanged);
    }
    public void Describe()
    {
        Owner.OnMovementAxisChanged.RemoveListener(OnMovementAxisChanged);
        transform.SetParent(_defaultParent);
        Owner = null;
    }

    private void OnMovementAxisChanged(Vector2 axis)
    {
        transform.localPosition = axis * _offset;
    }
}
