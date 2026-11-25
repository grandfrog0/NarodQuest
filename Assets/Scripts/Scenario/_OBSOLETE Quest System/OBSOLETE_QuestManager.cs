using UnityEngine;
using UnityEngine.Events;

public class OBSOLETE_QuestManager : MonoBehaviour
{
    public static OBSOLETE_QuestManager Instance { get; private set; }

    public UnityEvent<OBSOLETE_GameQuestStep> OnQuestChanged { get; } = new();

    private OBSOLETE_GameQuestStep _currentQuest = OBSOLETE_GameQuestStep.None;
    public OBSOLETE_GameQuestStep CurrentQuest
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
