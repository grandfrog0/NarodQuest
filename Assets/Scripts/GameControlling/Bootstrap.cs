using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LocatorManager _locatorManager;

    private void Start()
    {
        _locatorManager.Initialize();
    }
}
