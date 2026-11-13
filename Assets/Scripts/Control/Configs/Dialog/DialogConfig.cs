using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogConfig", menuName = "SO/Dialogs/Dialog Config")]
public class DialogConfig : ScriptableObject
{
    public List<LogSerializable> dialog;
    [SerializeField] private DialogConfig _nextDialogDefault = null;

    public DialogConfig NextDialog => OverrideConfig ?? _nextDialogDefault;
    public DialogConfig OverrideConfig { get; set; }
}