using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraConstraintsController : MonoBehaviour
{
    [SerializeField] private Transform _constraints;
    private CameraController _cameraController;
    private Transform _player;

    private Transform _current;
    private Bounds _currentBounds;

    public void Initialize(CameraController cameraController, Transform player)
    {
        _cameraController = cameraController;
        _player = player;
    }

    private void FixedUpdate()
    {
        if (_cameraController is null || _player is null)
        {
            return;
        }

        if ((_player.position - _currentBounds.ClosestPoint(_player.position)).sqrMagnitude == 0)
        {
            return;
        }

        foreach (Transform constraint in _constraints)
        {
            Bounds bounds = new Bounds(constraint.position, constraint.lossyScale);

            if (constraint != _current && (_player.position - bounds.ClosestPoint(_player.position)).sqrMagnitude == 0)
            {
                _current = constraint;
                _currentBounds = bounds;
                _cameraController.SetConstraints(_currentBounds);
                return;
            }
        }
    }

    private void Default()
    {
        Transform target = null;
        float targetSqrMagnitude = 0;
        Bounds targetBounds = default;

        foreach (Transform constraint in _constraints)
        {
            Bounds bounds = new Bounds(constraint.position, constraint.lossyScale);
            float sqrMagnitude = (_player.position - bounds.ClosestPoint(_player.position)).sqrMagnitude;

            if (sqrMagnitude == default)
            {
                target = constraint;
                targetSqrMagnitude = sqrMagnitude;
                targetBounds = bounds;

                break;
            }

            if (target is null || sqrMagnitude < targetSqrMagnitude)
            {
                target = constraint;
                targetSqrMagnitude = sqrMagnitude;
                targetBounds = bounds;
            }
        }

    }
}
