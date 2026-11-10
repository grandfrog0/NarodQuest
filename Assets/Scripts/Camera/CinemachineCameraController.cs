using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;
    [SerializeField] private CameraConstraints _constraints;

    private CinemachineFollow _camFollow;
    private CinemachineConfiner2D _camConfiner;

    public CameraConstraints GetConstraints()
    {
        return _constraints;
    }

    public void SetConstraints(CameraConstraints constraints)
    {
        _constraints = constraints;
        _camConfiner.BoundingShape2D = constraints.Collider;
    }

    public void ClearConstraints()
    {
        _constraints = null;
        _camConfiner.BoundingShape2D = null;
    }

    public void ApplyConfig(CameraConfig config)
    {
        _camFollow.FollowOffset = config.Offset;
        _camFollow.TrackerSettings.PositionDamping = Vector3.one * config.Speed;
    }

    public void Initialize()
    {
        _camFollow = GetComponent<CinemachineFollow>();
        _camConfiner = GetComponent<CinemachineConfiner2D>();

        ApplyConfig(_config);
        SetConstraints(_constraints);
    }
}
