using UnityEngine;
using UnityEngine.Events;

public class AntiTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger { get; } = new();
    [SerializeField] private PlayerBringable _target;
    [SerializeField] private float _radius;

    private void Start()
    {
        _target.OnDrop += OnTargetDrop;
    }

    private void OnTargetDrop(PlayerBringable target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < _radius)
        {
            OnTrigger.Invoke();
            _target.OnDrop -= OnTargetDrop;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
