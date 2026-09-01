using UnityEngine;

public class TutorialQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.Tutorial;

    [SerializeField] private DialogConfig _greetingsDialog;

    public TutorialQuestStep CurrentStep
    {
        get => _currentStep;
        set
        {
            _currentStep = value;
            Debug.Log($"Step: {_currentStep}");
            ManageStep(_currentStep);
        }
    }
    private TutorialQuestStep _currentStep;

    private DialogManager _dialogManager;

    protected override void StartQuest()
    {
        base.StartQuest();
        CurrentStep = TutorialQuestStep.Start;
        _dialogManager = DialogManager.Instance;
    }

    private void ManageStep(TutorialQuestStep step)
    {
        switch (step)
        {
            case TutorialQuestStep.Start:
                Greetings();
                break;

            case TutorialQuestStep.Movement:
                ShowMovementInstructions();
                break;

            case TutorialQuestStep.BringItem:
                ShowBringItemInstruction();
                break;

            case TutorialQuestStep.DragItem:
                ShowDragItemInstruction();
                break;

            case TutorialQuestStep.End:
                ShowEnd();
                break;
        }
    }

    private void Greetings()
    {
        // показать приветствие
        // после пропуска приветствия перейти в Movement

        DialogManager.Instance.StartDialog(_greetingsDialog, null);
        DialogManager.Instance.OnDialogEnd.AddListener(GoToMovementInstructions);

        void GoToMovementInstructions()
        {
            DialogManager.Instance.OnDialogEnd.RemoveListener(GoToMovementInstructions);
            CurrentStep = TutorialQuestStep.Movement;
        }
    }
    private void ShowMovementInstructions()
    {
        // показать инструкцию
        // после того, как игрок оттянет джойстик и дойдет до нужной точки перейти в BringItem


    }
    private void ShowBringItemInstruction()
    {
        // показать инструкцию
        // после того, как игрок подберет предмет в инвентарь перейти в DragItem
    }
    private void ShowDragItemInstruction()
    {
         // показать инструкцию
         // после того, как игрок перенесет камень в нужную точку перейти в End
    }
    private void ShowEnd()
    {
         // похвалить
         // завершить квест и перейти к следующему (дойти до избушки)
    }
}
