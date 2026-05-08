using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MilkCowConfig", menuName = "Scriptable Objects/MilkCowConfig")]
public class MilkCowConfig : ScriptableObject
{
    public float teatMinTimeMilking = 1f;
    public int countRounds;
    public float showingTeatDuration = 1f;
    public Color baseColor;
    public Color selectColor;
}
