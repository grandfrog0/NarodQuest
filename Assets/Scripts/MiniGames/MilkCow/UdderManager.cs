using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdderManager : MonoBehaviour
{
    [SerializeField] List<Teat> teats;
    [SerializeField] MilkCowConfig config;

    List<int> _sequence = new();

    public bool IsShowing { get; private set; }

    int _currentRound = 1;
    int _currentTeat = 0;

    void Start()
    {
        NextRound();
    }

    public void NextRound()
    {
        _sequence.Add(Random.Range(0, teats.Count));
        _currentRound++;

        StartCoroutine(ShowTeats());
    }

    IEnumerator ShowTeats()
    {
        IsShowing = true;

        foreach (int teatIndex in _sequence)
        {
            Debug.Log(teatIndex);
            yield return teats[teatIndex].ShowTeat();
        }

        IsShowing = false;
    }

    public void TeatMilked(int index)
    {
        if (_sequence[_currentTeat] == index)
        {
            _currentTeat++;
            if (_currentTeat == _sequence.Count)
            {
                _currentTeat = 0;
                NextRound();
            }
        }
        else
        {
            _currentTeat = 0;
        }
    }
}
