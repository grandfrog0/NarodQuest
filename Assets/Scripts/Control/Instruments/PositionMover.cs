using UnityEngine;

public class PositionMover : MonoBehaviour
{
    public void MoveTo(Transform target)
    {
        transform.position = target.position;
    }
}
