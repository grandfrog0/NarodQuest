using System;
using UnityEngine;

public class CowData : MonoBehaviour
{
    public event Action<CowData> OnCollisionEnter;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _model;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Transform Model => _model;
    public float StayTime { get; set; }
    public float MoveTime { get; set; }
    public float StartMoveTime { get; set; } = 1f;
    public float MoveSpeed { get; set; }
    public Vector2 MoveDirection { get; set; }
    public Vector3 Scale { get; set; } = Vector3.one;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(this);
    }
}
