using UnityEngine;

public class StepSkipper : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameQuestStep _firstStep;
    [SerializeField] private Transform _playerTransform;

    [Header("Tutorial")]
    [SerializeField] private Transform _tutorialPoint;

    [Header("Visit Granny")]
    [SerializeField] private Vector3 _hutPosition;

    [Header("Come to the Milkmaid")]
    [SerializeField] private Vector3 _milkmaidPosition;

    [Header("Come to the Field")]
    [SerializeField] private Vector3 _fieldPosition;

#if UNITY_EDITOR

    private void Awake()
    {
        if (_firstStep is not GameQuestStep.None)
        {
            Jump(_firstStep);
        }
    }

    public void Jump(GameQuestStep step)
    {
        switch (step)
        {
            case GameQuestStep.Tutorial:
                _playerTransform.position = _tutorialPoint.position;
                break;

            case GameQuestStep.VisitGranny:
                _playerTransform.position = _hutPosition;
                break;

            case GameQuestStep.ComeToTheMilkmaid:
                _playerTransform.position = _milkmaidPosition;
                break;

            case GameQuestStep.ComeToTheField:
                _playerTransform.position = _fieldPosition;
                break;
        }
    }

#endif
}
