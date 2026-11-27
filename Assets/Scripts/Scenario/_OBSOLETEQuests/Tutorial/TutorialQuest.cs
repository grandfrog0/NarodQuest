using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TutorialQuest : OBSOLETE_AbstractQuest
{
    public override OBSOLETE_GameQuestStep GameStep => OBSOLETE_GameQuestStep.Tutorial;

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

    private LocatorManager _locatorManager;
    private OutlineController _outlineController;

    [Inject]
    private void Construct(LocatorManager locatorManager, OutlineController outlineController)
    {
        _locatorManager = locatorManager;
        _outlineController = outlineController;
    }

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
        return;

        base.StartQuest();
        CurrentStep = TutorialQuestStep.Start;

        Disactivate();
    }

    private void Disactivate()
    {
        return;

        _movementTrigger.Disable();
        _targetItem.gameObject.SetActive(false);
        _dragItem.IsActive = false;
        _dropTrigger.gameObject.SetActive(false);
    }

    protected override void EndQuest()
    {
        base.EndQuest();
        _onCompleted.Invoke();
    }

    private void ManageStep(TutorialQuestStep step)
    {
        return;

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

    private void MarkTarget(IOutlineContainer target)
    {
        _outlineController.AddForced(target);
        _locatorManager.SetTarget(target.transform);
    }

    private void UnmarkTarget(IOutlineContainer target)
    {
        _outlineController.RemoveForced(target);
        _locatorManager.Clear();
    }

    private void Greetings()
    {
        // показать приветствие
        // после пропуска приветствия перейти в Movement

        _dialogManager.Show(_greetingsDialog, null);
        _dialogManager.OnDialogComplete.AddListener(GoToMovementInstructions);

        void GoToMovementInstructions()
        {
            _dialogManager.OnDialogComplete.RemoveListener(GoToMovementInstructions);
            CurrentStep = TutorialQuestStep.Movement;
        }
    }
    private void ShowMovementInstructions()
    {
        // показать инструкцию
        // после того, как игрок оттянет джойстик и дойдет до нужной точки перейти в BringItem

        _movementTrigger.Enable();
        MarkTarget(_movementTrigger);
        _dialogManager.Show(_movementDialog, null);
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

        MarkTarget(_targetItem);
        _dialogManager.Show(_bringItemDialog, null);
    }
    private void ShowDragItemInstruction()
    {
        // показать инструкцию
        // после того, как игрок перенесет камень в нужную точку перейти в End
        _dragItem.IsActive = true;
        _dropTrigger.gameObject.SetActive(true);

        MarkTarget(_dragItem);
        _dragItem.Bringable.OnDrag += OnDrag;

        _dropTrigger.OnTrigger.AddListener(OnTargetDragDropped);

        _dialogManager.Show(_dragItemDialog, null);

        void OnDrag(PlayerBringable _)
        {
            _dragItem.Bringable.OnDrag -= OnDrag;

            UnmarkTarget(_dragItem);
            MarkTarget(_dropTrigger);
        }
    }
    private void ShowEnd()
    {
        // похвалить
        // завершить квест и перейти к следующему (дойти до избушки)

        _dropTrigger.OnTrigger.RemoveListener(OnTargetDragDropped);
        //_dropTrigger.gameObject.SetActive(false);
        _dragItem.IsActive = false;

        _dialogManager.OnDialogComplete.AddListener(End);
        _dialogManager.Show(_endDialog, null);

        void End()
        {
            _dialogManager.OnDialogComplete.RemoveListener(End);
            EndQuest();
        }
    }

    public void OnMovementTargetReceived()
    {
        UnmarkTarget(_movementTrigger);
        CurrentStep = TutorialQuestStep.BringItem;
    }

    public void OnTargetItemBrought(ItemCountPair drop)
    {
        UnmarkTarget(_targetItem);
        CurrentStep = TutorialQuestStep.DragItem;
    }

    public void OnTargetDragDropped()
    {
        UnmarkTarget(_dropTrigger);

        CurrentStep = TutorialQuestStep.End;
    }

    private void SkipTo(TutorialQuestStep step)
    {
        return;

        if (CurrentState is OBSOLETE_StateOfQuest.Finished)
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
