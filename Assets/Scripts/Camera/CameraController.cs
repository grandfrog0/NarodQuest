using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] CameraConfig config;

    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = target.position + config.offsetFollow;
    }
}
