using System.Collections.Generic;
using UnityEngine;

public class ExchangeTheGoldQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.ExchangeTheGold;

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
