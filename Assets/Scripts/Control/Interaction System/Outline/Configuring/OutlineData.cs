
using UnityEngine;

public class OutlineData
{
    public Color ArrowForcedColor { get; }
    public Color ArrowHighlightedForcedColor { get; }
    public Color ArrowSelectedColor { get; }
    public Color ArrowHighlightedColor { get; }

    public Color OutlineForcedColor { get; }
    public Color OutlineHighlightedForcedColor { get; }
    public Color OutlineSelectedColor { get; }
    public Color OutlineHighlightedColor { get; }

    public Sprite ArrowSprite { get; }
    public Sprite OutlineSprite { get; }

    public int SortingOrder { get; }

    public Vector3 OutlineOffset { get; }
    public Vector3 ArrowOffset { get; }

    public Vector3 OutlineSize { get; }

    public OutlineData(
        Color arrowForcedColor, Color arrowHighlightedForcedColor, Color arrowSelectedColor, Color arrowHighlightedColor, 
        Color outlineForcedColor, Color outlineHighlightedForcedColor, Color outlineSelectedColor, Color outlineHighlightedColor, 
        Sprite arrowSprite, Sprite outlineSprite, int sortingOrder, Vector3 outlineOffset, Vector3 arrowOffset, Vector3 outlineSize)
    {
        ArrowForcedColor = arrowForcedColor;
        ArrowHighlightedForcedColor = arrowHighlightedForcedColor;
        ArrowSelectedColor = arrowSelectedColor;
        ArrowHighlightedColor = arrowHighlightedColor;
        
        OutlineForcedColor = outlineForcedColor;
        OutlineHighlightedForcedColor = outlineHighlightedForcedColor;
        OutlineSelectedColor = outlineSelectedColor;
        OutlineHighlightedColor = outlineHighlightedColor;

        ArrowSprite = arrowSprite;
        OutlineSprite = outlineSprite;
        SortingOrder = sortingOrder;
        OutlineOffset = outlineOffset;
        ArrowOffset = arrowOffset;
        OutlineSize = outlineSize;
    }
}