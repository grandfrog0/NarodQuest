using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Trigger : MonoBehaviour
{
    [SerializeField] protected UnityEvent _onEnter;
    [SerializeField] protected bool _useOnce;
    [SerializeField] protected float _radius;
    [SerializeField] protected CircleCollider2D _collider;
    private bool _useEnterEvent = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_useEnterEvent)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            _onEnter.Invoke();

            if (_useOnce)
            {
                _useEnterEvent = false;
            }
        }
    }

    protected virtual void Start()
    {
        _collider.radius = _radius;
    }
    protected virtual void OnValidate()
    {
        _collider = GetComponent<CircleCollider2D>();
        if (_collider)
        {
            _collider.radius = _radius;
        }
    }
}
