using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LocatorManager : MonoBehaviour
{
    private static LocatorManager _instance;

    [SerializeField] Transform _playerTransform;

    [SerializeField] LocatorMark _markPrefab;
    [SerializeField] Transform _markParent;
    private List<LocatorMark> _marks = new();
    private LocatorMark _mark;

    public static LocatorMark CreateMark()
    {
        LocatorMark mark = Instantiate(_instance._markPrefab, _instance._markParent);
        _instance._marks.Add(mark);
        return mark;
    }

    public static void DeleteMark(LocatorMark mark)
    {
        _instance._marks.Remove(mark);
        Destroy(mark.gameObject);
    }

    public static void AddTarget(Transform target)
    {
        LocatorMark mark = _instance._marks.FirstOrDefault(x => x.Target == target);
        if (mark == null)
        {
            mark = CreateMark();
            mark.Target = target;
        }
    }

    public static void RemoveTarget(Transform target)
    {
        LocatorMark mark = _instance._marks.FirstOrDefault(x => x.Target == target);
        if (mark != null)
        {
            DeleteMark(mark);
        }
    }

    public static void SetTarget(Transform target)
    {
        _instance._mark.Target = target;
    }

    public static void Clear()
    {
        _instance._mark.Clear();
    }

    public void Initialize()
    {
        _instance = this;

        _mark = Instantiate(_markPrefab, _markParent);
        _mark.Clear();

        _marks.Clear();
    }

    private void FixedUpdate()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(_playerTransform.position);
        playerScreenPosition.z = 0;

        foreach (LocatorMark mark in _marks)
        {
            mark.Handle(Time.deltaTime);
        }
        _mark.Handle(Time.deltaTime);
    }
}
