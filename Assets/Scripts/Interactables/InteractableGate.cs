using System.Collections;
using UnityEngine;

public class InteractableGate : InteractableObject
{
    public bool IsOpened
    { 
        get => _isOpened; 
        private set => _isOpened = value; 
    }
    [SerializeField] private bool _isOpened;
    [SerializeField] private Animator _animator;
    public override float SizeMultiplier => 3f;

    public override void Interact()
    {
        IsOpened ^= true;
        _animator.SetBool("IsOpened", IsOpened);
    }
}
