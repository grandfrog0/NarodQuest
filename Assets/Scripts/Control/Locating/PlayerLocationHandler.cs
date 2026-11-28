using System;
using UnityEngine;
using Zenject;

public class PlayerLocationHandler : MonoBehaviour, ILocationRelative
{
    // TODO: переделать в просто LocationRelativeObject на игроке;
    public event Action<Location> OnChanged;
    
    private LocationsContainer _locationsContainer;

    private Location _current;
    public Location Current
    {
        get => _current;
        set
        {
            _current = value;
            OnChanged?.Invoke(_current);
        }
    }

    public Location Location => Current;

    [Inject]
    private void Construct(LocationsContainer locationsContainer, Location defaultLocation)
    {
        _locationsContainer = locationsContainer;
        _current = defaultLocation;
    }

    public void SetById(string id)
    {
        Current = _locationsContainer.GetById(id);
    }
    public string GetId()
    {
        return Current?.Id ?? "Default";
    }
}