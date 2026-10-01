using UnityEngine;

public class VisitGrannyQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.VisitGranny;

    [Header("Preparing")]
    [SerializeField] private Transform _hutTransform;
    [Header("Main")]
    [SerializeField] private NPC _grannyNpc;

    public void ShowHintToStart()
    {
        StartQuest();

        LocatorManager.SetTarget(_hutTransform);
        _grannyNpc.OnDialogComplete += OnDialogEnd;

        void OnDialogEnd()
        {
            _grannyNpc.OnDialogComplete -= OnDialogEnd;
            EndQuest();
        }
    }
}
