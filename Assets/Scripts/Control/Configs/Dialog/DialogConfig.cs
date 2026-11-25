using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "SO/Dialogs/Dialog")]
public class DialogConfig : ScriptableObject
{
    public List<LogSerializable> dialog;
    [SerializeField] private DialogConfig _nextDialogDefault = null;

    public DialogConfig NextDialog => OverrideConfig ?? _nextDialogDefault;
    public DialogConfig OverrideConfig { get; set; }
}