using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OutlineController : MonoBehaviour
{
    private OutlineConfig _defaultOutline;
    private OutlineView.Factory _factory;

    private Dictionary<InteractableObject, OutlineView> _outlines = new();

    [Inject]
    private void Construct(OutlineView.Factory factory, OutlineConfig defaultOutline)
    {
        _factory = factory;
        _defaultOutline = defaultOutline;
    }

    public void AddTarget(InteractableObject target)
    {
        if (_outlines.ContainsKey(target))
        {
            return;
        }

        OutlineConfig config = target.Outline ?? _defaultOutline;

        OutlineView outline = _factory.Create(config, target.transform);
        Debug.Log(outline.transform.parent, outline);
        outline.State = OutlineState.Highlighted;

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
