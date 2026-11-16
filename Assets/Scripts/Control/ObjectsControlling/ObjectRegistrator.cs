using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRegistrator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _registrated;

    private void Awake()
    {
        ObjectRegistrationExtension.Construct(this);
    }

    public IEnumerable<GameObject> Enumerate()
    {
        return _registrated.AsEnumerable();
    }

    public int GetId(GameObject gameObject)
    {
        return _registrated.IndexOf(gameObject);
    }
    public GameObject Get(int id)
    {
        if (id >= 0 && id < _registrated.Count)
        {
            return _registrated[id];
        }

        return null;
    }
}
