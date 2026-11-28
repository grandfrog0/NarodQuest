using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LocatorManager : MonoBehaviour
{
    private Transform _player;
    private LocatorPositionCalculator _locatorPositionCalculator;

    [SerializeField] LocatorMark _markPrefab;
    [SerializeField] Transform _markParent;
    private List<LocatorMark> _marks = new();
    private LocatorMark _mark;

    [Inject]
    private void Construct(GameObject player, LocatorPositionCalculator locatorPositionCalculator)
    {
        _player = player.transform;
        _locatorPositionCalculator = locatorPositionCalculator;
    }

    public LocatorMark CreateMark()
    {
        LocatorMark mark = Instantiate(_markPrefab, _markParent);
        mark.Initialize(_locatorPositionCalculator);

        _marks.Add(mark);
        return mark;
    }

    public void DeleteMark(LocatorMark mark)
    {
        _marks.Remove(mark);
        Destroy(mark.gameObject);
    }

    public void AddTarget(Transform target)
    {
        LocatorMark mark = _marks.FirstOrDefault(x => x.Target == target);
        if (mark == null)
        {
            mark = CreateMark();
            mark.Target = target;
        }
    }

    public void RemoveTarget(Transform target)
    {
        LocatorMark mark = _marks.FirstOrDefault(x => x.Target == target);
        if (mark != null)
        {
            DeleteMark(mark);
        }
    }

    public void SetTarget(Transform target)
    {
        _mark.Target = target;
    }

    public void Clear()
    {
        _mark.Clear();
    }

    public void Initialize()
    {
        _mark = Instantiate(_markPrefab, _markParent);
        _mark.Clear();

        _marks.Clear();
    }

    private void FixedUpdate()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(_player.position);
        playerScreenPosition.z = 0;

        foreach (LocatorMark mark in _marks)
        {
            mark.Handle(Time.deltaTime);
        }
        _mark.Handle(Time.deltaTime);
    }
}
