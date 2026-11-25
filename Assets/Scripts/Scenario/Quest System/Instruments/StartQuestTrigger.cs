using UnityEngine;
using Zenject;

public class StartQuestTrigger : CollisionTrigger
{
    [SerializeField] private QuestInfo _questInfo;
    [Inject] private QuestLauncher _questLauncher;

    protected override void Trigger()
    {
        _questLauncher.TryLaunch(_questInfo);
        base.Trigger();
    }
}
