using System.Collections.Generic;
using UnityEngine;

public class VisitGrannyQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.VisitGranny;

    [Header("Preparing")]
    [SerializeField] private Transform _hutTransform;
    [Header("Main")]
    [SerializeField] private NPC _grannyNpc;

    private bool _isInsideHut;

    private LocatorMark Mark
    {
        get
        {
            if (_mark == null)
            {
                _mark = LocatorManager.CreateMark();
            }
            return _mark;
        }
    }
    private LocatorMark _mark;

    private Transform CurrentTarget => _isInsideHut ? _grannyNpc.transform : _hutTransform;

    public void ShowHintToStart()
    {
        StartQuest();

        RefreshMark();

        _grannyNpc.OnDialogComplete += OnDialogEnd;

        void OnDialogEnd()
        {
            _grannyNpc.OnDialogComplete -= OnDialogEnd;
            EndQuest();
        }
    }

    public void OnHutEnter()
    {
        _isInsideHut = true;
        RefreshMark();
    }

    public void OnHutExit()
    {
        _isInsideHut = false;
        RefreshMark();
    }

    public override List<string> GetTags()
    {
        List<string> result = base.GetTags();
        result.Add(_isInsideHut ? "1" : "0");
        return result;
    }

    public override void ApplyTags(List<string> tags)
    {
        base.ApplyTags(tags);
        
        _isInsideHut = tags[^1] == "1";

        if (CurrentState is StateOfQuest.Started)
        {
            ShowHintToStart();
        }
    }

    private void RefreshMark()
    {
        Mark.Target = CurrentTarget;
    }
}
