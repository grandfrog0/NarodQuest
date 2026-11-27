using System.Collections.Generic;
using UnityEngine;

public class OBSOLETE_QuestsRegistrator : MonoBehaviour
{
    [SerializeField] private List<OBSOLETE_AbstractQuest> _quests;
    public List<OBSOLETE_AbstractQuest> Quests => _quests;

    public void Initialize()
    {
        return;

        foreach (OBSOLETE_AbstractQuest quest in _quests)
        {
            quest.Initialize();
        }
    }
}
