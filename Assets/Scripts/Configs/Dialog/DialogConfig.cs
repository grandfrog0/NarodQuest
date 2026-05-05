using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogConfig", menuName = "Scriptable Objects/DialogConfig")]
public class DialogConfig : ScriptableObject
{
    public List<LogSerializable> dialog;
}