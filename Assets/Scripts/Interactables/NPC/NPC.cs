using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : InteractableObject, IDescriptable
{
    public event Action OnDialogComplete;

    [SerializeField] private List<CompanionSerializable> _companions;
    [SerializeField] private DialogConfig _dialog;

    public int Id => this.GetId();

    public override void Interact(InteractionData data)
    {
        IsActive = false;
        DialogManager.Instance.StartDialog(_dialog, _companions);

        DialogManager.Instance.OnDialogEnd.AddListener(OnDialogEnd);

        void OnDialogEnd()
        {
            DialogManager.Instance.OnDialogEnd.RemoveListener(OnDialogEnd);
            OnDialogComplete?.Invoke();
        }
    }

    public virtual List<string> GetTags()
    {
        return new List<string>();
    }

    public virtual void ApplyTags(List<string> tags)
    {
        
    }
}
