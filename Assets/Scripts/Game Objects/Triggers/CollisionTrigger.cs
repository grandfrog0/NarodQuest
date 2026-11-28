using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour, IOutlineContainer
{
    [SerializeField] private string _tag = "Player";
    [SerializeField] private UnityEvent _onTrigger;

    public UnityEvent OnTrigger => _onTrigger;
    public OutlineConfig Outline => null;

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && !collision.isTrigger)
        {
            Trigger();
        }
    }

    protected virtual void Trigger()
    {
        _onTrigger.Invoke();
    }
}
