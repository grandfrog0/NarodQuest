using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public UnityEvent<QuestState> OnQuestChanged { get; } = new();

    private QuestState _currentQuest = QuestState.None;
    public QuestState CurrentQuest
    {
        get => _currentQuest;
        set
        {
            if (_currentQuest != value)
            {
                _currentQuest = value;
                OnQuestChanged.Invoke(value);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        OnQuestChanged.AddListener(x => Debug.Log(x));
    }
}
