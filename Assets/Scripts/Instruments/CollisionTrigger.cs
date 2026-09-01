using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] private string _tag = "Player";
    public UnityEvent OnTrigger => _onTrigger;
    [SerializeField] private UnityEvent _onTrigger;

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
        if (collision.CompareTag(_tag))
        {
            _onTrigger.Invoke();
        }
    }
}
