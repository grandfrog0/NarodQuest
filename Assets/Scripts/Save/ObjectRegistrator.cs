using System.Collections.Generic;
using UnityEngine;

public class ObjectRegistrator : MonoBehaviour
{
    static private ObjectRegistrator _instance;
    static public int GetId(GameObject gameObject)
    {
        return _instance.GetIdByGameObject(gameObject);
    }
    static public GameObject Get(int id)
    {
        return _instance.GetById(id);
    }

    [SerializeField] private List<GameObject> _registrated;

    public void Initialize()
    {
        _instance = this;
    }

    private int GetIdByGameObject(GameObject gameObject)
    {
        return _registrated.IndexOf(gameObject);
    }

    private GameObject GetById(int id)
    {
        if (id >= 0 && id < _registrated.Count)
        {
            return null;
        }

        return _registrated[id];
    }
}
