using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour, IOutlineContainer
{
    public UnityEvent<bool> OnActiveChanged { get; } = new();
    public virtual bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnActiveChanged.Invoke(value);
        }
    }
    [SerializeField] private OutlineConfig _outline;
    [SerializeField] private bool _isActive = true;

    public OutlineConfig Outline => _outline;
    public virtual float SizeMultiplier => 1f;

    public abstract void Interact(InteractionData data);
}
