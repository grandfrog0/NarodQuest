using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogManagerConfig", menuName = "Scriptable Objects/DialogManagerConfig")]
public class DialogManagerConfig : ScriptableObject
{
    public Vector2 cloudWindowOffsset = new(0, 1f);
}
