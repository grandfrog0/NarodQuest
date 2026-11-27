using System;
using UnityEngine;
using Zenject;

public abstract class QuestStep : ScriptableObject
{
    public event EventHandler OnFinish;
    public abstract void Start();
    public void Skip()
    {
        HandleSkip();
        Finish();
    }
    protected void Finish()
    {
        OnFinish?.Invoke(this, EventArgs.Empty);
        HandleFinish();
    }

    /// <summary>
    /// Здесь можно сразу задать конечные состояния объектов
    /// </summary>
    protected abstract void HandleSkip();

    /// <summary>
    /// Здесь можно отписаться от событий
    /// </summary>
    protected abstract void HandleFinish();
}