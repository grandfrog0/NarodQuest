using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "questsNamings", menuName = "SO/Quests/Quests Namings")]
[Obsolete]
public class QuestsNamingsConfig : ScriptableObject
{
    public List<OBSOLETE_QuestNaming> Quests;

    private void OnValidate()
    {
        if (Quests.Count == 0)
        {
            foreach (OBSOLETE_GameQuestStep quest in Enum.GetValues(typeof(OBSOLETE_GameQuestStep)))
            {
                Quests.Add(new OBSOLETE_QuestNaming() { Quest = quest });
            }
        }
    }
}
