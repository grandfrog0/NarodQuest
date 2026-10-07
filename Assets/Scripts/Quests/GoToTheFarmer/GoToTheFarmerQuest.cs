using UnityEngine;

public class GoToTheFarmerQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.GoToTheFarmer;

    [SerializeField] private DialogConfig _dialog;

    protected override void StartQuest()
    {
        base.StartQuest();

        DialogManager.Instance.OnDialogEnd.AddListener(EndQuest);
        DialogManager.Instance.StartDialog(_dialog);
    }

    protected override void EndQuest()
    {
        DialogManager.Instance.OnDialogEnd.RemoveListener(EndQuest);

        base.EndQuest();
    }
}

