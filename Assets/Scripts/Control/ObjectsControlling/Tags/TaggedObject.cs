using UnityEngine;

public class TaggedObject : MonoBehaviour
{
    [SerializeField] private string _tag;
    public string Tag => _tag;
}
