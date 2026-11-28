using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DragDropItemQuestStep", menuName = "SO/Quests/Quest Steps/Drag Drop Item Quest Step")]
public class DragDropItemQuestStep : RequireTargetQuestStep<TakeableObject, DropTakeableTrigger>
{
    private BringableObjectController _bringableController;

    [Inject]
    private void Construct(BringableObjectController bringableController)
    {
        _bringableController = bringableController;
    }

    public override void Start()
    {
        Target2.gameObject.SetActive(true);
        Target2.OnTrigger.AddListener(OnDrop);

        if (_bringableController.Current == Target1.Bringable)
        {
            OnDrag(Target1.Bringable);
            return;
        }

        Target1.IsActive = true;

        Target1.Bringable.OnDrag += OnDrag;
        HintService.Mark(Target1);
    }

    private void OnDrag(PlayerBringable _)
    {
        Target1.Bringable.OnDrag -= OnDrag;
        HintService.Unmark(Target1);

        HintService.Mark(Target2);
    }

    private void OnDrop()
    {
        Target2.OnTrigger.RemoveListener(OnDrop);
        HintService.Unmark(Target2);

        Target1.IsActive = false;
        Finish();
    }

    protected override void HandleSkip()
    {
        Target1.IsActive = false;
        Target2.gameObject.SetActive(false);
    }

    protected override void HandleFinish()
    {
        HintService.Unmark(Target1);
        HintService.Unmark(Target2);

        Target1.Bringable.OnDrag -= OnDrag;
        Target2.OnTrigger.RemoveListener(OnDrop);
    }
}