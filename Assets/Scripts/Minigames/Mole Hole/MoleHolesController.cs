using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MoleHolesController : Minigame
{
    [SerializeField] private UnityEvent _onGameStart;
    [SerializeField] private UnityEvent _onGameEnd;
    [SerializeField] private UnityEvent _onWin;
    [SerializeField] private UnityEvent _onLose;
    [SerializeField] private UnityEvent<int> _onMolesCountChanged;
    [SerializeField] private UnityEvent<float> _onDigDurabilityChanged;

    [SerializeField] private List<MoleHole> _holes;
    [SerializeField] private Vector2 _actionCooldownRange;
    [SerializeField] private Vector2 _moleOutsideTimeRange;
    [SerializeField] private Vector2Int _maxMolesCountRange;
    [SerializeField] private int _maxDigDurability;
    private Coroutine _gameRoutine;
    private int _molesLeft;
    private int _digDurabilityLeft;

    private bool _isWon;

    private bool IsGameActive => _gameRoutine != null;
    private int CurrentMolesCount => _holes.Count(x => x.HoleState == MoleHoleState.MoleHole);

    public override List<string> GetTags()
    {
        List<string> result = base.GetTags();

        if (result[0] == "1")
        {
            result.Add(_isWon ? "Win" : "Lose");
        }

        return result;
    }

    public override void ApplyTags(List<string> tags)
    {
        base.ApplyTags(tags);

        if (tags.Count > 1)
        {
            _isWon = tags[1] == "Win";
        }
    }

    public void StartMinigame()
    {
        if (IsCompleted && _isWon)
        {
            return;
        }

        _molesLeft = Random.Range(_maxMolesCountRange.x, _maxMolesCountRange.y + 1);
        _onMolesCountChanged.Invoke(_molesLeft);

        _digDurabilityLeft = _maxDigDurability;
        _onDigDurabilityChanged.Invoke(Mathf.RoundToInt((float)_digDurabilityLeft / _maxDigDurability * 100));

        _gameRoutine = StartCoroutine(MinigameRoutine());

        foreach (MoleHole hole in _holes)
        {
            hole.Subscribe(() => OnInteract(hole));
        }

        _onGameStart.Invoke();
    }

    public void StopMinigame()
    {
        if (_gameRoutine != null)
        {
            StopCoroutine(_gameRoutine);
        }
        _gameRoutine = null;

        foreach (MoleHole hole in _holes)
        {
            hole.Unsubscribe();
        }

        if (_molesLeft <= 0)
        {
            Debug.Log("WIN");
            _isWon = true;
            _onWin.Invoke();
        }
        else
        {
            Debug.Log("LOSE");
            _isWon = false;
            _onLose.Invoke();
        }

        IsCompleted = true;
        _onGameEnd.Invoke();
    }

    private IEnumerator MinigameRoutine()
    {
        while (GetAvailableHole() != null)
        {
            yield return new WaitForSeconds(Random.Range(_actionCooldownRange.x, _actionCooldownRange.y));

            if (CurrentMolesCount < _molesLeft)
            {
                GetOutRandomHole();
            }
        }

        StopMinigame();
    }

    private MoleHole GetAvailableHole()
    {
        if (_holes.All(x => x.HoleState == MoleHoleState.Rock))
        {
            return null;
        }

        MoleHole current = null;
        while (current == null || current.HoleState == MoleHoleState.Rock)
        {
            current = _holes[Random.Range(0, _holes.Count)];
        }
        return current;
    }

    private void GetOutRandomHole()
    {
        MoleHole hole = GetAvailableHole();
        if (hole == null)
        {
            StopMinigame();
            return;
        }

        StartCoroutine(GetOutRandomHoleRoutine(hole));
    }

    private IEnumerator GetOutRandomHoleRoutine(MoleHole hole)
    {
        float time = Random.Range(_moleOutsideTimeRange.x, _moleOutsideTimeRange.y);

        hole.HoleState = MoleHoleState.MoleHole;

        yield return new WaitForSeconds(time);

        if (hole.HoleState == MoleHoleState.MoleHole)
        {
            if (Random.Range(0, 100) <= 50)
            {
                hole.HoleState = MoleHoleState.Rock;
            }
            else
            {
                hole.HoleState = MoleHoleState.Hole;
            }
        }
    }

    private void OnInteract(MoleHole hole)
    {
        if (!IsGameActive)
        {
            return;
        }

        switch (hole.HoleState)
        {
            case MoleHoleState.MoleHole:
                _molesLeft--;
                _onMolesCountChanged.Invoke(_molesLeft);
                hole.HoleState = MoleHoleState.Hole;
                if (_molesLeft <= 0)
                {
                    StopMinigame();
                }
                break;

            case MoleHoleState.Rock:
                _digDurabilityLeft--;
                _onDigDurabilityChanged.Invoke(Mathf.RoundToInt((float)_digDurabilityLeft / _maxDigDurability * 100));
                if (_digDurabilityLeft <= 0)
                {
                    StopMinigame();
                }
                break;
        }
    }
}
