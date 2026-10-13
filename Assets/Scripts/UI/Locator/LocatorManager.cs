using Unity.VisualScripting;
using UnityEngine;

public class LocatorManager : MonoBehaviour
{
    private static LocatorManager _instance;
    [SerializeField] LocatorMark _mark;

    public static void SetTarget(Transform target)
    {
        _instance._mark.SetTarget(target);
    }

    public static void Clear()
    {
        _instance._mark.Clear();
    }

    public void Initialize()
    {
        _instance = this;
        _mark.Clear();
    }
}
