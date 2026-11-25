using System.Collections.Generic;
using UnityEngine;

public class FinalQuest : OBSOLETE_AbstractQuest
{
    public override OBSOLETE_GameQuestStep GameStep => OBSOLETE_GameQuestStep.End;

    [SerializeField] private DialogConfig _dialog;

    protected override void StartQuest()
    {
        base.StartQuest();

        _dialogManager.OnDialogComplete.AddListener(EndQuest);
        _dialogManager.Show(_dialog);
    }

    protected override void EndQuest()
    {
        _dialogManager.OnDialogComplete.RemoveListener(EndQuest);

        base.EndQuest();
    }
}
