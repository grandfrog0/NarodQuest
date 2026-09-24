using UnityEngine;

public class VisitGrannyQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.VisitGranny;

    [Header("Preparing")]
    [SerializeField] private Transform _hutTransform;
    [Header("Main")]
    [SerializeField] private DialogConfig _visitGrannyDialog;

    public void ShowHintToStart()
    {
        LocatorManager.SetTarget(_hutTransform);
    }

    protected override void StartQuest()
    {
        base.StartQuest();

        LocatorManager.Clear();

        /*
        DialogManager.Instance.StartDialog(_visitGrannyDialog, null);
        DialogManager.Instance.OnDialogEnd.AddListener(OnDialogEnd);

        void OnDialogEnd()
        {
            DialogManager.Instance.OnDialogEnd.RemoveListener(OnDialogEnd);
            EndQuest();
        }
        */
    }
}
