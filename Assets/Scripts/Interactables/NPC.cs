using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject
{
    [SerializeField] List<CompanionSerializable> companionsSerializable;
    [SerializeField] DialogConfig dialog;
    [SerializeField] DialogManager dialogManager;

    Dictionary<Person, Transform> companions = new();

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        foreach (CompanionSerializable companionSerializable in companionsSerializable)
        {
            companions[companionSerializable.person] = companionSerializable.companion;
        }
    }

    public override void Interact()
    {
        IsActive = false;
        dialogManager.StartDialog(dialog, companions);
    }
}
