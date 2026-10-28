using System.Collections.Generic;
using UnityEngine;

public class QuestsRegistrator : MonoBehaviour
{
    [SerializeField] private List<AbstractQuest> _quests;
    public List<AbstractQuest> Quests => _quests;

    public void Initialize()
    {
        foreach (AbstractQuest quest in _quests)
        {
            quest.Initialize();
        }
    }
}
