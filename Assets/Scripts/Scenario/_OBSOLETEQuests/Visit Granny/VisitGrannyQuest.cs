using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VisitGrannyQuest : OBSOLETE_AbstractQuest
{
    public override OBSOLETE_GameQuestStep GameStep => OBSOLETE_GameQuestStep.VisitGranny;

    [Header("Preparing")]
    [SerializeField] private Transform _hutTransform;
    [Header("Main")]
    [SerializeField] private NPC _grannyNpc;
    private LocatorManager _locatorManager;
    private bool _isInsideHut;
    private LocatorMark _mark;

    private LocatorMark Mark
    {
        get
        {
            if (_mark == null)
            {
                _mark = _locatorManager.CreateMark();
            }
            return _mark;
        }
    }

    private Transform CurrentTarget => _isInsideHut ? _grannyNpc.transform : _hutTransform;

    [Inject]
    protected void Construct(LocatorManager locatorManager)
    {
        _locatorManager = locatorManager;
    }

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
        //result.Add(_isInsideHut ? "1" : "0");
        return result;
    }

    

    public override void ApplyTags(List<string> tags)
    {
        base.ApplyTags(tags);
        
        //_isInsideHut = tags[^1] == "1";

        //if (/*CurrentState is OBSOLETE_StateOfQuest.Started*/ false)
        //{
        //    ShowHintToStart();
        //}
    }

    private void RefreshMark()
    {
        Mark.Target = CurrentTarget;
    }
}
public class OBSOLETE_StateOfQuest
{
    public class Started { }
}