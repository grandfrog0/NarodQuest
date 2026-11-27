using System;
using Zenject;

public interface IQuest
{
    public event EventHandler OnFinished;

    public QuestInfo Info { get; }
    public QuestState State { get; }
    public int CurrentStep { get; }

    public bool TryStart();
    public void SkipTo(int stepIndex);

    public class Factory : PlaceholderFactory<QuestInfo, IQuest> { }
}