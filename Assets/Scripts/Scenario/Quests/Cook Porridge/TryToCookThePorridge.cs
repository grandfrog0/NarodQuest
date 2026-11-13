using UnityEngine;

public class TryToCookThePorridge : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.TryToCookThePorridge;

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
