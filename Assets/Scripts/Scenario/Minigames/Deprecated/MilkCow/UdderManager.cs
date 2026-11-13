using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UdderManager : MonoBehaviour
{
    [SerializeField] List<Teat> teats;
    [SerializeField] MilkCowConfig config;
    [SerializeField] TextMeshProUGUI round;
    [SerializeField] TextMeshProUGUI status;

    List<int> _sequence = new();

    public bool IsShowing { get; private set; }

    int _currentRound = 0;
    int _currentTeat = 0;

    void Start()
    {
        NextRound();
    }

    public void NextRound()
    {
        _currentRound++;
        if (_currentRound > config.countRounds)
        {
            status.text = "Win";
            return;
        }

        _sequence.Add(Random.Range(0, teats.Count));
        round.text = $"Round: {_currentRound}";
        StartCoroutine(ShowTeats());
    }

    IEnumerator ShowTeats()
    {
        IsShowing = true;

        foreach (int teatIndex in _sequence)
        {
            //Debug.Log(teatIndex);
            yield return teats[teatIndex].ShowTeat();
        }

        IsShowing = false;
    }

    public void TeatMilked(int index)
    {
        Debug.Log($"milked -- {index}");
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
            status.text = "Wrong";

            _currentTeat = 0;
            _currentRound = 0;
            _sequence.Clear();
            NextRound();
        }
    }
}
