using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestInfo", menuName = "SO/Quests/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private List<QuestStartCondition> _startConditions;
    [SerializeField] private List<QuestStep> _steps;
    [SerializeField] private List<QuestFinishAction> _endActions;

    public IReadOnlyList<QuestStartCondition> StartConditions => _startConditions;
    public IReadOnlyList<QuestStep> Steps => _steps;
    public IReadOnlyList<QuestFinishAction> EndActions => _endActions;
}
