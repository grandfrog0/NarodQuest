using System.Collections.Generic;
using UnityEngine;

public class TryToTrickTheRobbersQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.TryToTrickTheRobbers;

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
