using UnityEngine;

public class TryToCookThePorridge : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.TryToCookThePorridge;

    [SerializeField] private DialogConfig _dialog;

    protected override void StartQuest()
    {
        base.StartQuest();

        _dialogManager.OnDialogEnd.AddListener(EndQuest);
        _dialogManager.StartDialog(_dialog);
    }

    protected override void EndQuest()
    {
        _dialogManager.OnDialogEnd.RemoveListener(EndQuest);

        base.EndQuest();
    }
}
