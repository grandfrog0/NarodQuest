using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoleHolesController : MonoBehaviour
{
    [SerializeField] private List<MoleHole> _holes;
    [SerializeField] private Vector2 _actionCooldownRange;
    [SerializeField] private Vector2 _moleOutsideTimeRange;
    [SerializeField] private Vector2Int _maxMolesCountRange;
    [SerializeField] private int _maxDigDurability;
    private Coroutine _gameRoutine;
    private int _molesLeft;
    private int _digDurabilityLeft;

    private void Start()
    {
        StartMinigame();
    }

    public void StartMinigame()
    {
        _molesLeft = Random.Range(_maxMolesCountRange.x, _maxMolesCountRange.y + 1);
        _digDurabilityLeft = _maxDigDurability;
        _gameRoutine = StartCoroutine(MinigameRoutine());

        foreach (MoleHole hole in _holes)
        {
            hole.Subscribe(() => OnInteract(hole));
        }
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
            hole.Describe();
        }

        if (_molesLeft <= 0)
        {
            Debug.Log("WIN");
        }
        else
        {
            Debug.Log("LOSE");
        }
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

    private int CurrentMolesCount => _holes.Count(x => x.HoleState == MoleHoleState.MoleHole);

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
        switch (hole.HoleState)
        {
            case MoleHoleState.MoleHole:
                _molesLeft--;
                hole.HoleState = MoleHoleState.Hole;
                if (_molesLeft <= 0)
                {
                    StopMinigame();
                }
                break;

            case MoleHoleState.Rock:
                _digDurabilityLeft--;
                if (_digDurabilityLeft <= 0)
                {
                    StopMinigame();
                }
                break;
        }
    }
}
