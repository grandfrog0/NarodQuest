using UnityEngine;

public class CameraConstraints : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private string _name;
    public Collider2D Collider => _collider;
    public string Name => _name;
}
