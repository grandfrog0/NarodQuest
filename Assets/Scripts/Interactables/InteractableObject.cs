using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    public UnityEvent<bool> OnActiveChanged { get; } = new();
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnActiveChanged.Invoke(value);
        }
    }
    private bool _isActive = true;
    public abstract void Interact();
}
