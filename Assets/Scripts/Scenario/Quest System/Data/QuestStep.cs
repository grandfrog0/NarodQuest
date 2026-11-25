using System;
using UnityEngine;
using Zenject;

public abstract class QuestStep : ScriptableObject
{
    public event EventHandler OnFinish;
    public abstract void Start();
    protected void Finish()
    {
        OnFinish?.Invoke(this, EventArgs.Empty);
    }
}