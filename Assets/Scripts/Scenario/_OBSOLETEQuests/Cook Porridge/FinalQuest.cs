using System.Collections.Generic;
using UnityEngine;

public abstract class OBSOLETE_AbstractQuest : MonoBehaviour
{
    public virtual OBSOLETE_GameQuestStep GameStep { get; }
    protected virtual void StartQuest() { }
    protected virtual void EndQuest() { }
    protected DialogManager _dialogManager;

    public virtual List<string> GetTags() { return null; }
    public virtual void ApplyTags(List<string> tags) { }
}

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

