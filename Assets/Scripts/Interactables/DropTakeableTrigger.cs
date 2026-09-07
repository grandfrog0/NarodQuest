using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class DropTakeableTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger => _onTrigger;
    [SerializeField] private UnityEvent _onTrigger = new();

    [SerializeField] string _targetTag;
    private PlayerBringable _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_targetTag) && collision.TryGetComponent(out _target))
        {
            _target.OnDrop += OnTargetDrop;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_target != null && collision.gameObject == _target.gameObject)
        {
            _target.OnDrop -= OnTargetDrop;
            _target = null;
        }
    }

    private void OnTargetDrop()
    {
        OnTrigger.Invoke();
    }
}
