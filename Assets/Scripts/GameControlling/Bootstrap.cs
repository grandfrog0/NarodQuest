using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LocatorManager _locatorManager;
    [SerializeField] private TransitionManager _transitionManager;

    private void Start()
    {
        _locatorManager.Initialize();
        _transitionManager.Initialize();
    }
}
