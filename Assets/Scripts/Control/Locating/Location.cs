using System;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public CameraConstraints Constraints { get; private set; }
    [field: SerializeField] public List<PlayerTranslator> Exits { get; private set; }

    private void Awake()
    {
        foreach (PlayerTranslator exit in Exits)
        {
            exit.Initialize(this);
        }
    }
}