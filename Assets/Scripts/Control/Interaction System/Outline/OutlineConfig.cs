using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Outline", menuName = "SO/Outline Config")]
public class OutlineConfig : ScriptableObject
{
    public Sprite OutlineSprite;
    public Color OutlineColor;

    public Sprite ArrowSprite;
    public Color ArrowColor;

    public Vector3 Offset;
    public Vector3 Size;

    public int SortingOrder;
}
