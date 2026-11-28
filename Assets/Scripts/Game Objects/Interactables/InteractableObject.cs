using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour, IOutlineContainer
{
    public event EventHandler<InteractionData> OnInteract;
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

    public void Interact(InteractionData data)
    {
        HandleInteraction(data);
        OnInteract?.Invoke(this, data);
    }
    protected abstract void HandleInteraction(InteractionData data);
}
