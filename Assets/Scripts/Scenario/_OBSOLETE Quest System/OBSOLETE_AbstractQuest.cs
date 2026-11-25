using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public abstract class OBSOLETE_AbstractQuest : MonoBehaviour
{
    [SerializeField] private bool _isTrigger = true;

    public abstract OBSOLETE_GameQuestStep GameStep { get; }
    public OBSOLETE_StateOfQuest CurrentState { get; protected set; }

    public string Name => GetType().Name;

    private bool _isInitialized = false;
    private OBSOLETE_StateOfQuest? _waitingState = null;

    protected DialogManager _dialogManager;

    protected virtual void StartQuest() => CurrentState = OBSOLETE_StateOfQuest.Started;
    protected virtual void Pause() => CurrentState = OBSOLETE_StateOfQuest.Paused;
    protected virtual void Continue() => CurrentState = OBSOLETE_StateOfQuest.Started;
    protected virtual void EndQuest() => CurrentState = OBSOLETE_StateOfQuest.Finished;

    [Inject]
    protected virtual void Construct(DialogManager dialogManager)
    {
        _dialogManager = dialogManager;
    }

    public void TryStartQuest()
    {
        if (!_isInitialized)
        {
            _waitingState = OBSOLETE_StateOfQuest.Started;
            Debug.Log(_waitingState);
            return;
        }

        switch (CurrentState)
        {
            case OBSOLETE_StateOfQuest.Started or OBSOLETE_StateOfQuest.Finished:
                return;

            case OBSOLETE_StateOfQuest.Unstarted:
                StartQuest();
                break;

            case OBSOLETE_StateOfQuest.Paused:
                Continue();
                break;
        }
    }

    public virtual void TryPauseQuest()
    {
        switch (CurrentState)
        {
            case OBSOLETE_StateOfQuest.Started:
                Pause();
                break;

            case OBSOLETE_StateOfQuest.Unstarted or OBSOLETE_StateOfQuest.Finished or OBSOLETE_StateOfQuest.Paused:
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTrigger && collision.CompareTag("Player") && !collision.isTrigger)
        {
            TryStartQuest();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isTrigger && collision.CompareTag("Player") && !collision.isTrigger)
        {
            TryPauseQuest();
        }
    }

    public virtual List<string> GetTags()
    {
        return new List<string>
        {
            CurrentState.ToString()
        };
    }
    public virtual void ApplyTags(List<string> tags)
    {
        CurrentState = Enum.Parse<OBSOLETE_StateOfQuest>(tags[0]);
    }

    public void Initialize()
    {
        _isInitialized = true;
        if (_waitingState is OBSOLETE_StateOfQuest.Started)
        {
            TryStartQuest();
        }
    }
}
