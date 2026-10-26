using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGate : InteractableObject, IDescriptable
{
    public bool IsOpened
    { 
        get => _isOpened; 
        private set => _isOpened = value; 
    }
    [SerializeField] private bool _isOpened;
    [SerializeField] private Animator _animator;
    public override float SizeMultiplier => 3f;

    public override void Interact(InteractionData data)
    {
        IsOpened ^= true;
        _animator.SetBool("IsOpened", IsOpened);
    }

    public int Id => this.GetId();

    public List<string> GetTags()
    {
        return new List<string> { IsOpened ? "1" : "0" };
    }

    public void ApplyTags(List<string> tags)
    {
        Debug.Log(tags);
        IsOpened = tags[0] == "1";
        _animator.SetBool("IsOpened", IsOpened);
    }
}
