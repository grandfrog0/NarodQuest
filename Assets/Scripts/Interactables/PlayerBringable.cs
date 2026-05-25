using UnityEngine;

public class PlayerBringable : MonoBehaviour
{
    private Transform _defaultParent;

    private void Start()
    {
        _defaultParent = transform.parent;
    }
    public void Drop()
    {
        transform.SetParent(_defaultParent);
    }
}
