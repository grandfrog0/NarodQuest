using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTrigger;
    public UnityEvent OnTrigger => _onTrigger;

    public void Trigger()
    {
        OnTrigger.Invoke();
    }
}
