using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOutlineGraphics", menuName = "SO/Outline/Outline Graphics")]
public class OutlineGraphicsConfig : ScriptableObject
{
    [SerializeField] private Sprite _outlineSprite;
    [SerializeField] private Sprite _arrowSprite;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _forcedColor;
    [SerializeField, Range(0, 1)] private float _outlineOpacity;
    [SerializeField, Range(0, 1)] private float _arrowOpacity;
    [SerializeField] private int _sortingOrder;

    public Sprite OutlineSprite => _outlineSprite;
    public Sprite ArrowSprite => _arrowSprite;
    public Color DefaultColor => _defaultColor;
    public Color ForcedColor => _forcedColor;
    public float OutlineOpacity => _outlineOpacity;
    public float ArrowOpacity => _arrowOpacity;
    public int SortingOrder => _sortingOrder;
}
