using Unity.Collections;
using UnityEngine;

public class HintService
{
    private OutlineController _outlineController;
    private LocatorManager _locatorManager;

    public HintService(OutlineController outlineController, LocatorManager locatorManager)
    {
        _outlineController = outlineController;
        _locatorManager = locatorManager;
    }

    public void Mark(GameObject target)
    {
        _locatorManager.AddTarget(target.transform);
    }
    public void Unmark(GameObject target)
    {
        _locatorManager.RemoveTarget(target.transform);
    }

    public void Mark(IOutlineContainer target)
    {
        _outlineController.AddForced(target);
        _locatorManager.AddTarget(target.transform);
    }

    public void Unmark(IOutlineContainer target)
    {
        _outlineController.RemoveForced(target);
        _locatorManager.RemoveTarget(target.transform);
    }
}
