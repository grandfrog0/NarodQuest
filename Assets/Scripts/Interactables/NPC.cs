using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : InteractableObject
{
    [SerializeField] private List<CompanionSerializable> _companions;
    [SerializeField] private DialogConfig _dialog;

    public override void Interact()
    {
        IsActive = false;
        DialogManager.Instance.StartDialog(_dialog, _companions);
    }
}
