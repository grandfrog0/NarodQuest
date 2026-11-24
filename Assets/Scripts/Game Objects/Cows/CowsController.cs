using UnityEngine;
using UnityEngine.EventSystems;

public class CowsController : MonoBehaviour
{
    [SerializeField] private Vector2 _stayTimeRange;
    [SerializeField] private Vector2 _moveTimeRange;
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private float _stepPeriod;
    [SerializeField] private float _stepAngle;
    [SerializeField] private float _stepMagnitude;
    [SerializeField] private CowData[] _cows;

    private void Start()
    {
        foreach (CowData cow in _cows)
        {
            cow.OnCollisionEnter += OnCowCollisionEnter;

            cow.StayTime = Random.Range(_stayTimeRange.x, _stayTimeRange.y);
        }
    }

    private void FixedUpdate()
    {
        foreach (CowData cow in _cows)
        {
            switch (cow.MoveTime)
            {
                case <= 0:
                    switch (cow.StayTime)
                    {
                        case <= 0:
                            cow.MoveDirection = Random.insideUnitCircle.normalized;
                            cow.StartMoveTime = Random.Range(_moveTimeRange.x, _moveTimeRange.y);
                            cow.MoveTime = cow.StartMoveTime;
                            cow.MoveSpeed = Random.Range(_speedRange.x, _speedRange.y);

                            float movementAxis = cow.MoveDirection.x >= 0 ? 1 : -1;
                            cow.Scale = new Vector3(movementAxis * Mathf.Abs(cow.Model.localScale.x), cow.Model.localScale.y, 1);
                            break;

                        case > 0:
                            cow.StayTime -= Time.fixedDeltaTime;
                            cow.Rigidbody.linearVelocity = Vector2.zero;
                            break;
                    }
                    break;

                case > 0:
                    cow.Rigidbody.linearVelocity = cow.MoveDirection * cow.MoveSpeed;
                    float sin = Mathf.Sin(Time.time / _stepPeriod);
                    cow.Model.rotation = Quaternion.Euler(0, 0, sin * _stepAngle);
                    cow.Model.localPosition = new Vector3(-sin * _stepMagnitude * 2f, Mathf.Abs(sin) * _stepMagnitude * 2f);

                    cow.Model.localScale = Vector3.Lerp(cow.Model.localScale, cow.Scale, 1f - cow.MoveTime / cow.StartMoveTime);

                    cow.MoveTime -= Time.fixedDeltaTime;

                    if (cow.MoveTime <= 0)
                    {
                        cow.Rigidbody.linearVelocity = Vector2.zero;
                        cow.Model.rotation = Quaternion.identity;
                        cow.Model.localPosition = Vector3.zero;

                        cow.Model.localScale = cow.Scale;

                        cow.StayTime = Random.Range(_stayTimeRange.x, _stayTimeRange.y);
                    }
                    break;
            }
        }
    }

    private void OnCowCollisionEnter(CowData cow)
    {
        if (Random.Range(0, 100) < 25)
        {
            cow.MoveTime = 0.001f;
        }
    }

    private void OnDestroy()
    {
        foreach (CowData cow in _cows)
        {
            cow.OnCollisionEnter -= OnCowCollisionEnter;
        }
    }
}
