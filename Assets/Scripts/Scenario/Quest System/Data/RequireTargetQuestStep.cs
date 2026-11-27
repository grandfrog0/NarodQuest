
using UnityEngine;
using Zenject;

public abstract class RequireTargetQuestStep<T> : QuestStep where T : Component
{
    [SerializeField] private string _targetTag;

    protected T Target { get; private set; }
    protected HintService HintService { get; private set; }

    [Inject]
    private void Construct(TaggedObjectsContainer container, HintService hintService)
    {
        Target = container.Get<T>(_targetTag);
        HintService = hintService;
    }
}

public abstract class RequireTargetQuestStep<T1, T2> : QuestStep where T1 : Component where T2 : Component
{
    [SerializeField] private string _target1Tag;
    [SerializeField] private string _target2Tag;

    protected T1 Target1 { get; private set; }
    protected T2 Target2 { get; private set; }
    protected HintService HintService { get; private set; }

    [Inject]
    private void Construct(TaggedObjectsContainer container, HintService hintService)
    {
        Target1 = container.Get<T1>(_target1Tag);
        Target2 = container.Get<T2>(_target2Tag);
        HintService = hintService;
    }
}
