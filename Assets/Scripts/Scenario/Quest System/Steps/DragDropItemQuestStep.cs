using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DragDropItemQuestStep", menuName = "SO/Quests/Quest Steps/Drag Drop Item Quest Step")]
public class DragDropItemQuestStep : RequireTargetQuestStep<TakeableObject, DropTakeableTrigger>
{
    public override void Start()
    {
        Target1.IsActive = true;
        Target2.gameObject.SetActive(true);

        Target1.Bringable.OnDrag += OnDrag;
        Target2.OnTrigger.AddListener(OnDrop);

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

    public override void Skip()
    {
        Target1.IsActive = false;
        Target2.gameObject.SetActive(false);
    }

    //TODO
    private void OnDestroy()
    {
        Target1.Bringable.OnDrag -= OnDrag;
        Target2.OnTrigger.RemoveListener(OnDrop);
    }
}