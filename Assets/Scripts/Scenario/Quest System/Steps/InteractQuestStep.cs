using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewInteractQuestStep", menuName = "SO/Quests/Quest Steps/Interact Quest Step")]
public class InteractQuestStep : RequireTargetQuestStep<InteractableObject>
{
    public override void Start()
    {
        Target.OnInteract += OnInteract;
        HintService.Mark(Target);
    }

    private void OnInteract(object _, InteractionData e)
    {
        Target.OnInteract -= OnInteract;
        HintService.Unmark(Target);

        Finish();
    }

    protected override void HandleSkip() { }

    protected override void HandleFinish()
    {
        HintService.Unmark(Target);
        Target.OnInteract -= OnInteract;
        Target.OnInteract -= OnInteract;
    }
}
