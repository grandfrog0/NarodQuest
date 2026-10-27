using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractQuest : MonoBehaviour
{
    [SerializeField] private bool _isTrigger = true;

    public abstract GameQuestStep GameStep { get; }
    public StateOfQuest CurrentState { get; protected set; }

    public string Name => GetType().Name;

    private bool _isInitialized = false;
    private StateOfQuest? _waitingState = null;

    protected virtual void StartQuest() => CurrentState = StateOfQuest.Started;
    protected virtual void Pause() => CurrentState = StateOfQuest.Paused;
    protected virtual void Continue() => CurrentState = StateOfQuest.Started;
    protected virtual void EndQuest() => CurrentState = StateOfQuest.Finished;

    public void TryStartQuest()
    {
        if (!_isInitialized)
        {
            _waitingState = StateOfQuest.Started;
            return;
        }

        switch (CurrentState)
        {
            case StateOfQuest.Started or StateOfQuest.Finished:
                return;

            case StateOfQuest.Unstarted:
                StartQuest();
                break;

            case StateOfQuest.Paused:
                Continue();
                break;
        }
    }

    public virtual void TryPauseQuest()
    {
        switch (CurrentState)
        {
            case StateOfQuest.Started:
                Pause();
                break;

            case StateOfQuest.Unstarted or StateOfQuest.Finished or StateOfQuest.Paused:
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
        CurrentState = Enum.Parse<StateOfQuest>(tags[0]);

        _isInitialized = true;
        if (_waitingState is StateOfQuest.Started)
        {
            TryStartQuest();
        }
    }
}
