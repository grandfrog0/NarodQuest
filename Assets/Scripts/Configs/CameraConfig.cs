using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Scriptable Objects/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    public Vector3 Offset = new(0, 0, -10f);
    public float Speed = 4;
}
