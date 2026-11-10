using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    public static CinemachineCameraController Instance { get; private set; }

    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;
    [SerializeField] private CameraConstraints _constraints;

    private CinemachineCamera _cam;
    private CinemachineFollow _camFollow;
    private CinemachineConfiner2D _camConfiner;

    public float CameraSize
    {
        get => _cam.Lens.OrthographicSize;
        set => _cam.Lens.OrthographicSize = value;
    }

    public CameraConstraints GetConstraints()
    {
        return _constraints;
    }

    public void SetConstraints(CameraConstraints constraints)
    {
        _constraints = constraints;
        _camConfiner.BoundingShape2D = constraints.Collider;

        if (constraints.CameraSize != 0)
        {
            CameraSize = constraints.CameraSize;
        }
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

    public void Move(Vector3 delta)
    {
        _cam.OnTargetObjectWarped(_target.transform, delta);
    }

    public void Initialize()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _cam = GetComponent<CinemachineCamera>();
        _camFollow = GetComponent<CinemachineFollow>();
        _camConfiner = GetComponent<CinemachineConfiner2D>();

        ApplyConfig(_config);
        SetConstraints(_constraints);
    }
}
