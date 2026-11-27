using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class Quest : IQuest
{
    public event EventHandler OnFinished;

    private QuestInfo _info;
    private QuestState _state;

    private DiContainer _container;

    public int CurrentStep { get; private set; }

    public QuestInfo Info
    {
        get => _info;
        private set => _info = value;
    }
    public QuestState State
    {
        get => _state;
        private set
        {
            _state = value;
            Debug.Log($"Quest state: {_state}");
        }
    }

    public Quest(QuestInfo info, DiContainer container)
    {
        Info = info;
        CurrentStep = -1;

        _container = container;
    }

    public bool TryStart()
    {
        if (State is QuestState.None && Info.StartConditions.All(x => x.IsReady()))
        {
            Start();
            return true;
        }

        return false;
    }

    public void SkipTo(int stepIndex)
    {
        for (CurrentStep = 0; CurrentStep < stepIndex; CurrentStep++)
        {
            if (CurrentStep >= Info.Steps.Count)
            {
                Finish();
                return;
            }

            QuestStep step = Info.Steps[CurrentStep];

            _container.Inject(step);
            step.Skip();

            Debug.Log($"Step {CurrentStep} skipped");
        }

        CurrentStep = stepIndex;
        Debug.Log($"Skipped to step {CurrentStep}");
    }

    private void Start()
    {
        CurrentStep = -1;
        State = QuestState.Started;
        Next();
    }

    private void Next()
    {
        CurrentStep++;

        if (CurrentStep >= Info.Steps.Count)
        {
            Finish();
            return;
        }

        QuestStep step = Info.Steps[CurrentStep];

        _container.Inject(step);
        step.Start();
        step.OnFinish += OnStepFinish;

        Debug.Log($"Step {CurrentStep} started");
    }

    private void OnStepFinish(object sender, EventArgs e)
    {
        if (CurrentStep >= Info.Steps.Count)
        {
            return;
        }

        QuestStep step = Info.Steps[CurrentStep];
        step.OnFinish -= OnStepFinish;

        Debug.Log($"Step {CurrentStep} finished");

        Next();
    }

    private void Finish()
    {
        foreach (QuestFinishAction action in Info.EndActions)
        {
            action.Invoke();
        }

        OnFinished?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return $"Quest ({Info.name})";
    }
}