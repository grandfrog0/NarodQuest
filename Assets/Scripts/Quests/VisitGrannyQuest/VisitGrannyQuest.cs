using UnityEngine;

public class VisitGrannyQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.VisitGranny;

    [SerializeField] private Transform _hutTransform;

    public void ShowHintToStart()
    {
        LocatorManager.SetTarget(_hutTransform);
    }

    protected override void StartQuest()
    {
        base.StartQuest();

        LocatorManager.Clear();
    }
}
