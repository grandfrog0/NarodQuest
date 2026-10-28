using UnityEngine;

public class CowsController : MonoBehaviour
{
    [SerializeField] private Vector2 _stayTimeRange;
    [SerializeField] private Vector2 _moveTimeRange;
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private float _stepPeriod;
    [SerializeField] private float _stepAngle;
    [SerializeField] private float _stepMagnitude;
    [SerializeField] private CowData[] _cows;

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
                            cow.MoveTime = Random.Range(_moveTimeRange.x, _moveTimeRange.y);
                            cow.MoveSpeed = Random.Range(_speedRange.x, _speedRange.y);
                            break;

                        case > 0:
                            cow.StayTime -= Time.fixedDeltaTime;
                            break;
                    }
                    break;

                case > 0:
                    cow.Rigidbody.linearVelocity = cow.MoveDirection * cow.MoveSpeed;
                    float sin = Mathf.Sin(Time.time / _stepPeriod);
                    cow.Model.rotation = Quaternion.Euler(0, 0, sin * _stepAngle);
                    cow.Model.localPosition = new Vector3(-sin * _stepMagnitude, Mathf.Abs(sin) * _stepMagnitude);

                    cow.MoveTime -= Time.fixedDeltaTime;

                    if (cow.MoveTime <= 0)
                    {
                        cow.Rigidbody.linearVelocity = Vector2.zero;
                        cow.Model.rotation = Quaternion.identity;
                        cow.Model.localPosition = Vector3.zero;

                        cow.StayTime = Random.Range(_stayTimeRange.x, _stayTimeRange.y);
                    }
                    break;
            }
        }
    }
}
