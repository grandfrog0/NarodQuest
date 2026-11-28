using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestInfo", menuName = "SO/Quests/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private List<QuestStartCondition> _startConditions;
    [SerializeField] private List<QuestStep> _steps;
    [SerializeField] private List<QuestFinishAction> _endActions;

    public string Id => string.IsNullOrWhiteSpace(_id) ? name : _id;
    public IReadOnlyList<QuestStartCondition> StartConditions => _startConditions;
    public IReadOnlyList<QuestStep> Steps => _steps;
    public IReadOnlyList<QuestFinishAction> EndActions => _endActions;
}
