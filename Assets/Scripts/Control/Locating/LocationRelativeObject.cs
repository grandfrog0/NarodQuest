using UnityEngine;

public class LocationRelativeObject : MonoBehaviour, ILocationRelative
{
    [field: SerializeField] public Location Location { get; set; }
}

// TODO: предметы, которые несет игрок, меняют локацию вместе с ним
// TODO: если на объекте нет скрипта, то он по умолчанию в Default