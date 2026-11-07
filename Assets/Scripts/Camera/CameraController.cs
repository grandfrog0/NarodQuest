using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;
    [SerializeField] private CameraConstraints _constraints;
    [SerializeField] private Bounds[] _constraintBounds;

    private Camera _cam;

    public CameraConstraints GetConstraints()
    {
        return _constraints;
    }

    public void SetConstraints(Bounds[] constraintBounds)
    {
        _constraintBounds = constraintBounds;
    }

    public void SetConstraints(CameraConstraints constraints)
    {
        _constraints = constraints;
    }
    public void ClearConstraints()
    {
        _constraints = null;
    }

    public Vector3 TargetPosition
    {
        get
        {
            if (_constraintBounds == default)
            {
                return _target.position + _config.Offset;
            }

            float height = _cam.orthographicSize;
            float width = height * _cam.aspect;

            return new Vector3(
                Mathf.Clamp(_target.position.x + _config.Offset.x, _constraintBounds.min.x + width, _constraintBounds.max.x - width),
                Mathf.Clamp(_target.position.y + _config.Offset.y, _constraintBounds.min.y + height, _constraintBounds.max.y - height),
                _config.Offset.z);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _config.Speed * Time.deltaTime);
    }

    public void Initialize()
    {
        transform.position = TargetPosition;
        _cam = Camera.main;
    }
}
