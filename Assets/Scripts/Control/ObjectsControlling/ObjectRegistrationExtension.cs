using UnityEngine;

public static class ObjectRegistrationExtension
{
    public static int GetId(this MonoBehaviour mono)
    {
        return ObjectRegistrator.GetId(mono.gameObject);
    }
}
