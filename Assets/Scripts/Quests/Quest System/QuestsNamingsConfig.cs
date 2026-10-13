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
            foreach (GameQuestStep quest in Enum.GetValues(typeof(GameQuestStep)))
            {
                Quests.Add(new QuestNaming() { Quest = quest });
            }
        }
    }
}
