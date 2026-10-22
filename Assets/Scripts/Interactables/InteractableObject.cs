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
    public OutlineConfig Outline => _outline;
    [SerializeField] private OutlineConfig _outline;
    public int SelectionOrder => _selectionOrder;
    [SerializeField] private int _selectionOrder;
    public virtual float SizeMultiplier => 1f;
    public abstract void Interact(InteractionData data);
}
