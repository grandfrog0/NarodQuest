using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewReachPointQuestStep", menuName = "SO/Quests/Quest Steps/Reach Point Quest Step")]
public class ReachPointQuestStep : RequireTargetQuestStep<CollisionTrigger>
{
    public override void Start()
    {
        Target.OnTrigger.AddListener(OnTrigger);
        Target.Enable();

        HintService.Mark(Target);
    }

    private void OnTrigger()
    {
        Target.OnTrigger.RemoveListener(OnTrigger);
        HintService.Unmark(Target);

        Finish();
    }

    protected override void HandleSkip()
    {
        Target.Disable();
    }

    protected override void HandleFinish()
    {
        HintService.Unmark(Target);
        Target.OnTrigger.RemoveListener(OnTrigger);
    }
}
