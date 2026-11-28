using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewNpcQuestStep", menuName = "SO/Quests/Quest Steps/Npc Quest Step")]
public class NpcQuestStep : RequireTargetQuestStep<NPC>
{
    public override void Start()
    {
        Target.OnDialogComplete += OnDialogComplete;
        HintService.Mark(Target);
    }

    private void OnDialogComplete()
    {
        Target.OnDialogComplete -= OnDialogComplete;
        HintService.Unmark(Target);

        Finish();
    }

    protected override void HandleSkip() { }

    protected override void HandleFinish()
    {
        HintService.Unmark(Target);
        Target.OnDialogComplete -= OnDialogComplete;
    }
}
