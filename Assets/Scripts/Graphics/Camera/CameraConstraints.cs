using UnityEngine;

public class CameraConstraints : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private string _name;
    [SerializeField] private float _cameraSize;
    public Collider2D Collider => _collider;
    public string Name => _name;
    public float CameraSize => _cameraSize;
}
