using UnityEngine;
using Zenject;

public static class ObjectRegistrationExtension
{
    private static ObjectRegistrator _objectRegistrator;

    public static void Construct(ObjectRegistrator objectRegistrator)
    {
        _objectRegistrator = objectRegistrator;
    }

    public static int GetId(this MonoBehaviour mono) 
    {
        return _objectRegistrator.GetId(mono.gameObject);
    }
}
