using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraConstraintsRegistrator : MonoBehaviour
{
    [SerializeField] private List<CameraConstraints> _constraints;

    public CameraConstraints GetByName(string name)
    {
        return _constraints.FirstOrDefault(x => x.Name == name);
    }
}
