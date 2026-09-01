using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;

    private Vector3 TargetPosition => _target.position + _config.Offset;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, _config.Speed * Time.deltaTime);
    }

    private void Start()
    {
        transform.position = TargetPosition;
    }
}
