using System;
using Zenject;

public interface IQuest
{
    event EventHandler OnFinished;

    QuestInfo Info { get; }
    QuestState State { get; }
    int CurrentStep { get; }

    void Initialize(QuestState state, int stepIndex);
    bool TryStart();

    class Factory : PlaceholderFactory<QuestInfo, IQuest> { }
}