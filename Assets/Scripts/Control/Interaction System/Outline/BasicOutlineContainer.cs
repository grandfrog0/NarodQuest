using UnityEngine;

public class BasicOutlineContainer : IOutlineContainer
{
    public Transform transform { get; }
    public OutlineConfig Outline { get; }

    public BasicOutlineContainer(Transform transform, OutlineConfig outline)
    {
        this.transform = transform;
        Outline = outline;
    }
}