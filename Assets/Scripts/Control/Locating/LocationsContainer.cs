using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationsContainer : MonoBehaviour
{
    [SerializeField] private List<Location> _locations;
    public IReadOnlyList<Location> Locations => _locations;

    public Location GetById(string id)
    {
        return _locations.FirstOrDefault(x => x.Id == id);
    }
}