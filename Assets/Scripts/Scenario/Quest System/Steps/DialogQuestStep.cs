using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "NewDialogQuestStep", menuName = "SO/Quests/Quest Steps/Dialog Quest Step")]
public class DialogQuestStep : QuestStep
{
    [SerializeField] private DialogConfig _dialogConfig;

    private DialogManager _dialogManager;

    [Inject]
    private void Construct(DialogManager dialogManager)
    {
        _dialogManager = dialogManager;
    }

    public override void Start()
    {
        _dialogManager.Show(_dialogConfig);
        _dialogManager.OnDialogComplete.AddListener(OnDialogComplete);
    }

    private void OnDialogComplete()
    {
        _dialogManager.OnDialogComplete.RemoveListener(OnDialogComplete);
        Finish();
    }
}
