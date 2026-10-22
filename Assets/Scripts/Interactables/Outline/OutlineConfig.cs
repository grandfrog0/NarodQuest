using UnityEngine;

[CreateAssetMenu(fileName = "Outline", menuName = "SO/Outline Config")]
public class OutlineConfig : ScriptableObject
{
    public Sprite Sprite;
    public Color Color;
    public Vector3 Offset;
    public Vector3 Size;
    public int SortingOrder;
}
