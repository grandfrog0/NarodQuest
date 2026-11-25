using System;
using System.Linq;
using TMPro;
using UnityEngine;

[Obsolete]
public class QuestVisualizer : MonoBehaviour
{
    [SerializeField] private FormattedText _currentQuestText;
    [SerializeField] private QuestsNamingsConfig _questsNamings;

    private void Start()
    {
        ShowQuest(OBSOLETE_QuestManager.Instance.CurrentQuest);
        OBSOLETE_QuestManager.Instance.OnQuestChanged.AddListener(ShowQuest);
    }

    public void ShowQuest(OBSOLETE_GameQuestStep quest)
    {
        if (quest == OBSOLETE_GameQuestStep.None)
        {
            _currentQuestText.Clear();
        }
        else
        {
            _currentQuestText.SetValue(_questsNamings.Quests.FirstOrDefault(x => x.Quest == quest)?.Naming ?? "");
        }
    }
}
