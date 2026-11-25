using UnityEngine;
using Zenject;

public class StepSkipper : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private OBSOLETE_GameQuestStep _firstStep;
    private Transform _playerTransform;

    [Header("Tutorial")]
    [SerializeField] private Transform _tutorialPoint;

    [Header("Visit Granny")]
    [SerializeField] private Vector3 _hutPosition;

    [Header("Come to the Milkmaid")]
    [SerializeField] private Vector3 _milkmaidPosition;

    [Header("Come to the Field")]
    [SerializeField] private Vector3 _fieldPosition;

#if UNITY_EDITOR

    [Inject]
    private void Construct(GameObject player)
    {
        _playerTransform = player.transform;
    }

    private void Awake()
    {
        if (_firstStep is not OBSOLETE_GameQuestStep.None)
        {
            Jump(_firstStep);
        }
    }

    public void Jump(OBSOLETE_GameQuestStep step)
    {
        switch (step)
        {
            case OBSOLETE_GameQuestStep.Tutorial:
                _playerTransform.position = _tutorialPoint.position;
                break;

            case OBSOLETE_GameQuestStep.VisitGranny:
                _playerTransform.position = _hutPosition;
                break;

            case OBSOLETE_GameQuestStep.ComeToTheMilkmaid:
                _playerTransform.position = _milkmaidPosition;
                break;

            case OBSOLETE_GameQuestStep.ComeToTheField:
                _playerTransform.position = _fieldPosition;
                break;
        }
        Debug.Log($"Player translated: {_playerTransform.position} ({_milkmaidPosition}) {step} ");
    }

#endif
}
