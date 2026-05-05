using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool IsActive { get; set; } = true;
    public abstract void Interact();
}
