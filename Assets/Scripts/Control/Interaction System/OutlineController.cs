using System.Collections.Generic;
using UnityEngine;

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
        outline.State = OutlineState.Highlighted;
        outline.Initalize(target.Outline ?? _defaultOutlineConfig);

        _outlines[target] = outline;
    }

    public void RemoveTarget(InteractableObject target)
    {
        if (_outlines.Remove(target, out OutlineView outline))
        {
            outline.Dispose();
        }
    }

    public void SelectTarget(InteractableObject target)
    {
        if (!_outlines.ContainsKey(target))
        {
            AddTarget(target);
        }

        _outlines[target].State = OutlineState.Selected;
    }

    public void DeselectTarget(InteractableObject target)
    {
        if (!_outlines.ContainsKey(target))
        {
            return;
        }

        _outlines[target].State = OutlineState.Highlighted;
    }

    public void OnBeforeTargetInteracting(InteractableObject target)
    {
        if (!_outlines.ContainsKey(target))
        {
            AddTarget(target);
        }

        _outlines[target].Interact();
    }
}
