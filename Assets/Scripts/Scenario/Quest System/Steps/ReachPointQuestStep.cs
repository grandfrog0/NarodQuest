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

    public override void Skip()
    {
        Target.Disable();
    }

    private void OnDestroy()
    {
        Target.OnTrigger.RemoveListener(OnTrigger);
    }
}
