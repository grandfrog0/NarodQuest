using System;
using UnityEngine;

[Obsolete]
public class QuestTrigger : Trigger
{
    [SerializeField] private OBSOLETE_GameQuestStep _quest;

    protected override void Start()
    {
        base.Start();
        _onEnter.AddListener(() => OBSOLETE_QuestManager.Instance.CurrentQuest = _quest);
        _useOnce = true;
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        _useOnce = true;
    }
}
