using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialQuest : AbstractQuest
{
    public override GameQuestStep GameStep => GameQuestStep.Tutorial;

    [SerializeField] private UnityEvent _onCompleted;

    [Header("Start")]
    [SerializeField] private DialogConfig _greetingsDialog;
    [Header("Movement")]
    [SerializeField] private DialogConfig _movementDialog;
    [SerializeField] private CollisionTrigger _movementTrigger;
    [Header("BringItem")]
    [SerializeField] private DialogConfig _bringItemDialog;
    [SerializeField] private DroppedItem _targetItem;
    [Header("DragItem")]
    [SerializeField] private DialogConfig _dragItemDialog;
    [SerializeField] private TakeableObject _dragItem;
    [SerializeField] private DropTakeableTrigger _dropTrigger;
    [Header("End")]
    [SerializeField] private DialogConfig _endDialog;

    public TutorialQuestStep CurrentStep
    {
        get => _currentStep;
        set
        {
            _currentStep = value;
            ManageStep(_currentStep);
        }
    }
    private TutorialQuestStep _currentStep;

    private void Start()
    {
        Disactivate();
    }

    protected override void StartQuest()
    {
        base.StartQuest();
        CurrentStep = TutorialQuestStep.Start;

        Disactivate();
    }

    private void Disactivate()
    {
        _movementTrigger.Disable();
        _targetItem.gameObject.SetActive(false);
        _dragItem.gameObject.SetActive(false);
        _dropTrigger.gameObject.SetActive(false);
    }

    protected override void EndQuest()
    {
        base.EndQuest();
        _onCompleted.Invoke();
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

        _movementTrigger.Enable();
        LocatorManager.SetTarget(_movementTrigger.transform);
        DialogManager.Instance.StartDialog(_movementDialog, null);
    }
    private void ShowBringItemInstruction()
    {
        // показать инструкцию
        // после того, как игрок подберет предмет в инвентарь перейти в DragItem

        if (_targetItem.IsPicked)
        {
            OnTargetItemBrought(null);
            return;
        }

        _targetItem.gameObject.SetActive(true);
        _targetItem.OnPicked += OnTargetItemBrought;

        DialogManager.Instance.StartDialog(_bringItemDialog, null);
    }
    private void ShowDragItemInstruction()
    {
        // показать инструкцию
        // после того, как игрок перенесет камень в нужную точку перейти в End

        _dragItem.gameObject.SetActive(true);
        _dropTrigger.gameObject.SetActive(true);

        LocatorManager.SetTarget(_dragItem.transform);
        _dragItem.Bringable.OnDrag += OnDrag;

        _dropTrigger.OnTrigger.AddListener(OnTargetDragDropped);

        DialogManager.Instance.StartDialog(_dragItemDialog, null);

        void OnDrag(PlayerBringable _)
        {
            _dragItem.Bringable.OnDrag -= OnDrag;

            LocatorManager.SetTarget(_dropTrigger.transform);
        }
    }
    private void ShowEnd()
    {
        // похвалить
        // завершить квест и перейти к следующему (дойти до избушки)

        _dropTrigger.OnTrigger.RemoveListener(OnTargetDragDropped);
        _dropTrigger.gameObject.SetActive(false);
        _dragItem.IsActive = false;

        DialogManager.Instance.OnDialogEnd.AddListener(End);
        DialogManager.Instance.StartDialog(_endDialog, null);

        void End()
        {
            DialogManager.Instance.OnDialogEnd.RemoveListener(End);
            EndQuest();
        }
    }

    public void OnMovementTargetReceived()
    {
        CurrentStep = TutorialQuestStep.BringItem;
        //_movementTrigger.Disable();
        LocatorManager.Clear();
    }

    public void OnTargetItemBrought(ItemCountPair drop)
    {
        Debug.Log(drop);
        CurrentStep = TutorialQuestStep.DragItem;
    }

    public void OnTargetDragDropped()
    {
        LocatorManager.Clear();

        CurrentStep = TutorialQuestStep.End;
    }

    private void SkipTo(TutorialQuestStep step)
    {
        if (CurrentState is StateOfQuest.Finished)
        {
            return;
        }

        switch (step)
        {
            case TutorialQuestStep.Start:
                Greetings();
                break;

            case TutorialQuestStep.Movement:
                ShowMovementInstructions();
                break;

            case TutorialQuestStep.BringItem:
                SkipMovement();
                ShowBringItemInstruction();
                break;

            case TutorialQuestStep.DragItem:
                SkipBringItem();
                ShowDragItemInstruction();
                break;

            case TutorialQuestStep.End:
                SkipDragItem();
                ShowEnd();
                break;
        }

        void SkipMovement()
        {
            _movementTrigger.Disable();
                Debug.Log(_movementTrigger.gameObject.activeSelf);
        }
        void SkipBringItem()
        {
            SkipMovement();
        }
        void SkipDragItem()
        {
            SkipBringItem();

            _dragItem.gameObject.SetActive(false);
            _dropTrigger.gameObject.SetActive(false);
        }
    }

    public override List<string> GetTags()
    {
        List<string> result = base.GetTags();
        result.Add(CurrentStep.ToString());
        return result;
    }

    public override void ApplyTags(List<string> tags)
    {
        base.ApplyTags(tags);

        TutorialQuestStep step = Enum.Parse<TutorialQuestStep>(tags[1]);
        SkipTo(step);
    }
}
