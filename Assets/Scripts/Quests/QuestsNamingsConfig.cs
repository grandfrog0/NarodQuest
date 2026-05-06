using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "questsNamings", menuName = "SO/Quests/Quests Namings")]
public class QuestsNamingsConfig : ScriptableObject
{
    public List<QuestNaming> Quests;

    private void OnValidate()
    {
        if (Quests.Count == 0)
        {
            foreach (QuestState quest in Enum.GetValues(typeof(QuestState)))
            {
                Quests.Add(new QuestNaming() { Quest = quest });
            }
        }
    }
}
