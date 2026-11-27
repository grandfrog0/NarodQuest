using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PickItemQuestStep", menuName = "SO/Quests/Quest Steps/Pick Item Quest Step")]
public class PickItemQuestStep : RequireTargetQuestStep<DroppedItem>
{
    public override void Start()
    {
        Target.IsActive = true;
        Target.OnPicked += OnPicked;

        HintService.Mark(Target);
    }

    private void OnPicked(ItemCountPair _)
    {
        HintService.Unmark(Target);
        Target.OnPicked -= OnPicked;

        Finish();
    }
}
