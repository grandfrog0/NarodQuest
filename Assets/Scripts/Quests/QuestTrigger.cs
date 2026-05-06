using UnityEngine;

public class QuestTrigger : Trigger
{
    [SerializeField] private QuestState _quest;

    protected override void Start()
    {
        base.Start();
        _onEnter.AddListener(() => QuestManager.Instance.CurrentQuest = _quest);
        _useOnce = true;
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        _useOnce = true;
    }
}
