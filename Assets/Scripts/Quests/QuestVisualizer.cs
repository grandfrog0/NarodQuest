using System.Linq;
using TMPro;
using UnityEngine;

public class QuestVisualizer : MonoBehaviour
{
    [SerializeField] private FormattedText _currentQuestText;
    [SerializeField] private QuestsNamingsConfig _questsNamings;

    private void Start()
    {
        ShowQuest(QuestManager.Instance.CurrentQuest);
        QuestManager.Instance.OnQuestChanged.AddListener(ShowQuest);
    }

    public void ShowQuest(QuestState quest)
    {
        _currentQuestText.SetValue(_questsNamings.Quests.FirstOrDefault(x => x.Quest == quest)?.Naming ?? "");
    }
}
