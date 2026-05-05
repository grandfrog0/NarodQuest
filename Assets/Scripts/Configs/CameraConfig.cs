using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Scriptable Objects/CameraConfig")]
public class CameraConfig : ScriptableObject
{
    public Vector3 offsetFollow = new(0, 0, -10f);
}
