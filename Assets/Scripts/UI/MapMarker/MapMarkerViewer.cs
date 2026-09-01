using System.Collections.Generic;
using UnityEngine;

public class MapMarkerViewer : MonoBehaviour
{
    [SerializeField] MarkerPin _markerPinPrefab;
    [SerializeField] RectTransform _markersParent;

    [SerializeField] private List<MapMarker> _markers = new();
    private List<MarkerPin> _markerPins = new();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (MapMarker marker in _markers)
        {
            CreateMarkerPin(marker);
        }
    }

    public void AddMarker(MapMarker marker)
    {
        _markers.Add(marker);
        CreateMarkerPin(marker);
    }

    private void CreateMarkerPin(MapMarker marker)
    {
        MarkerPin markerPin = Instantiate(_markerPinPrefab, _markersParent);
        markerPin.SetIcon(marker.Icon);
        // TODOOOO
    }
}
