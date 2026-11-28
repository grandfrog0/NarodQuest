using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PickItemQuestStep", menuName = "SO/Quests/Quest Steps/Pick Item Quest Step")]
public class PickItemQuestStep : RequireTargetQuestStep<DroppedItem>
{
    private InventoryManager _inventoryManager;

    [Inject]
    private void Construct(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }

    public override void Start()
    {
        Target.TryUnpick(_inventoryManager.Remove);
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

    protected override void HandleSkip()
    {
        Target.MarkPickedUp(true);
    }

    protected override void HandleFinish()
    {
        HintService.Unmark(Target);
        Target.OnPicked -= OnPicked;
    }
}
