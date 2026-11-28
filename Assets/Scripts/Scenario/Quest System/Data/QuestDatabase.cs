using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestDatabase", menuName = "SO/Quests/Quest Database")]
public class QuestDatabase : ScriptableObject
{
    [SerializeField] private List<QuestInfo> _quests;
    private Dictionary<string, QuestInfo> _questsById;

    private void Initialize()
    {
        _questsById = new();

        foreach (QuestInfo info in _quests)
        {
            if (info != null)
            {
                Debug.Log(info.Id);
                _questsById[info.Id] = info;
            }
        }
    }

    public QuestInfo GetById(string id)
    {
        if (_questsById == null)
        {
            Initialize();
        }

        if (_questsById.TryGetValue(id, out QuestInfo info))
        {
            return info;
        }

        Debug.LogError($"Квест с Id {id} не был найден.");
        return null;
    }
}