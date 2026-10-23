using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraConstraintsRegistrator", menuName = "SO/Camera/Camera Constraints Registrator")]
public class CameraConstraintsRegistrator : ScriptableObject
{
    [SerializeField] private List<CameraConstraints> _constraints;
    private Dictionary<string, CameraConstraints> _pairs;

    public void Initialize()
    {
        _pairs = new();
        foreach (CameraConstraints constraint in _constraints)
        {
            _pairs[constraint.name] = constraint;
        }
    }

    public CameraConstraints GetByName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && _pairs.TryGetValue(name, out CameraConstraints value) ? value : null;
    }
}
