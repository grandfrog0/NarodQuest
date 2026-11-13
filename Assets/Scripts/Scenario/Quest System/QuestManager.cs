using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public UnityEvent<GameQuestStep> OnQuestChanged { get; } = new();

    private GameQuestStep _currentQuest = GameQuestStep.None;
    public GameQuestStep CurrentQuest
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
