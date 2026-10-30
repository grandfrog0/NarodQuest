using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private OutlineView _selectionOutlinePrefab;
    [SerializeField] private OutlineConfig _defaultOutlineConfig;

    private Dictionary<InteractableObject, OutlineView> _outlines = new();

    public void AddTarget(InteractableObject target)
    {
        if (_outlines.ContainsKey(target))
        {
            return;
        }

        OutlineView outline = Instantiate(_selectionOutlinePrefab, target.transform);
        outline.Initalize(target.Outline ?? _defaultOutlineConfig);

        _outlines[target] = outline;
    }

    public void RemoveTarget(InteractableObject target)
    {
        if (_outlines.Remove(target, out OutlineView outline))
        {
            Destroy(outline.gameObject);
        }
    }
}
