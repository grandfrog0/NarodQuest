using System;
using UnityEngine;

public abstract class QuestStartCondition : ScriptableObject
{
    public abstract bool IsReady();
}