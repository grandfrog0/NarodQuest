using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestInfo", menuName = "SO/Quests/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private List<QuestCondition> _conditions;
    [SerializeField] private List<QuestStep> _steps;
    [SerializeField] private List<QuestReward> _rewards;

    public string Id => string.IsNullOrWhiteSpace(_id) ? name : _id;
    public IReadOnlyList<QuestCondition> Conditions => _conditions;
    public IReadOnlyList<QuestStep> Steps => _steps;
    public IReadOnlyList<QuestReward> Rewards => _rewards;
}
