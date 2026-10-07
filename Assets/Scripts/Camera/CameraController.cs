using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;
    [SerializeField] private CameraConstraints _constraints;

    private Camera _cam;

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
            if (_constraints is null)
            {
                return _target.position + _config.Offset;
            }

            float height = _cam.orthographicSize;
            float width = height * _cam.aspect;

            Debug.Log(_constraints.Bounds.min.x);

            return new Vector3(
                Mathf.Clamp(_target.position.x + _config.Offset.x, _constraints.Bounds.min.x + width, _constraints.Bounds.max.x - width),
                Mathf.Clamp(_target.position.y + _config.Offset.y, _constraints.Bounds.min.y + height, _constraints.Bounds.max.y - height),
                _config.Offset.z);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _config.Speed * Time.deltaTime);
    }

    private void Start()
    {
        transform.position = TargetPosition;
        _cam = Camera.main;
    }
}
