using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OutlineController
{
    private OutlineConfig _defaultOutline;
    private OutlineView.Factory _factory;

    private Dictionary<IOutlineContainer, OutlineView> _outlines = new();

    [Inject]
    private void Construct(OutlineView.Factory factory, OutlineConfig defaultOutline)
    {
        _factory = factory;
        _defaultOutline = defaultOutline;
    }

    public OutlineView AddForced(IOutlineContainer target)
    {
        OutlineView outlineView = _outlines.TryGetValue(target, out OutlineView view) ? view : AddTarget(target);
        outlineView.SetForced(true);

        return outlineView;
    }

    public OutlineView RemoveForced(IOutlineContainer target)
    {
        if (_outlines.Remove(target, out OutlineView outline))
        {
            outline.Dispose();
        }

        return outline;
    }

    public OutlineView AddTarget(IOutlineContainer target)
    {
        if (_outlines.ContainsKey(target))
        {
            return null;
        }

        OutlineConfig config = target.Outline ?? _defaultOutline;
        OutlineData data = config.CreateData();

        OutlineView outline = _factory.Create(data, target.transform);
        outline.State = OutlineState.Highlighted;

        _outlines[target] = outline;

        return outline;
    }

    public OutlineView RemoveTarget(IOutlineContainer target)
    {
        if (_outlines.TryGetValue(target, out OutlineView outline))
        {
            if (outline.IsForced)
            {
                return null;
            }

            _outlines.Remove(target);
            outline.Dispose();
        }

        return outline;
    }

    public void SelectTarget(IOutlineContainer target)
    {
        if (!_outlines.ContainsKey(target))
        {
            AddTarget(target);
        }

        _outlines[target].State = OutlineState.Selected;
    }

    public void DeselectTarget(IOutlineContainer target)
    {
        if (!_outlines.ContainsKey(target))
        {
            return;
        }

        _outlines[target].State = OutlineState.Highlighted;
    }

    public void OnBeforeTargetInteracting(IOutlineContainer target)
    {
        if (!_outlines.ContainsKey(target))
        {
            AddTarget(target);
        }

        _outlines[target].Interact();
    }
}
