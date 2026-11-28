using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraConfig _config;

    private CinemachineCamera _cam;
    private CinemachineFollow _camFollow;
    private CinemachineConfiner2D _camConfiner;

    private PlayerLocationHandler _playerLocationHandler;

    public float CameraSize
    {
        get => _cam.Lens.OrthographicSize;
        set => _cam.Lens.OrthographicSize = value;
    }

    private Vector3 _lastTargetPosition;

    [Inject]
    private void Construct(PlayerLocationHandler playerLocationHandler)
    {
        _playerLocationHandler = playerLocationHandler;
        _playerLocationHandler.OnChanged += OnLocationChanged;
    }

    public void Initialize()
    {
        _cam = GetComponent<CinemachineCamera>();
        _camFollow = GetComponent<CinemachineFollow>();
        _camConfiner = GetComponent<CinemachineConfiner2D>();

        ApplyConfig(_config);
        OnLocationChanged(_playerLocationHandler.Current);
    }

    private void Update()
    {
        _lastTargetPosition = _target.position;
    }

    private void Move(Vector3 delta)
    {
        _cam.OnTargetObjectWarped(_target.transform, delta);
    }

    private void SetConstraints(CameraConstraints constraints)
    {
        _camConfiner.BoundingShape2D = constraints.Collider;

        if (constraints.CameraSize != 0)
        {
            CameraSize = constraints.CameraSize;
        }
    }

    public void ApplyConfig(CameraConfig config)
    {
        _camFollow.FollowOffset = config.Offset;
        _camFollow.TrackerSettings.PositionDamping = Vector3.one * config.Speed;
    }

    private void OnLocationChanged(Location location)
    {
        SetConstraints(location.Constraints);
        Move(_target.position - _lastTargetPosition);
    }

    private void OnDestroy()
    {
        _playerLocationHandler.OnChanged -= OnLocationChanged;
    }
}
