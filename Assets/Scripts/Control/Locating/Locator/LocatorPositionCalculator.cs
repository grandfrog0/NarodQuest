using System.Linq;
using UnityEngine;

public class LocatorPositionCalculator
{
    private ILocationRelative _player;
    private Location _defaultLocation;

    public LocatorPositionCalculator(PlayerLocationHandler playerLocationHandler, Location defaultLocation)
    {
        _player = playerLocationHandler;
        _defaultLocation = defaultLocation;
    }

    public Vector3 Calculate(Vector3 targetPosition, Location targetLocation = default)
    {
        if (targetLocation == null)
        {
            targetLocation = _defaultLocation;
        }

        if (_player.Location == targetLocation)
        {
            return targetPosition;
        }

        foreach (PlayerTranslator exit in _player.Location.Exits)
        {
            if (exit.Other.Location == targetLocation)
            {
                return exit.transform.position;
            }
        }

        return _player.Location.Exits.FirstOrDefault(x => x.Location == _defaultLocation).transform.position;
    }
}