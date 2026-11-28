using UnityEngine;

[CreateAssetMenu(fileName = "Outline", menuName = "SO/Outline Config")]
public class OutlineConfig : ScriptableObject
{
    [SerializeField] private OutlineGraphicsConfig _graphics;

    [SerializeField] private Vector3 _outlineOffset;
    [SerializeField] private Vector3 _arrowOffset;
    [SerializeField] private Vector3 _outlineSize;

    public OutlineGraphicsConfig Graphics => _graphics;
    public Vector3 OutlineOffset => _outlineOffset;
    public Vector3 ArrowOffset => _arrowOffset;
    public Vector3 OutlineSize => _outlineSize;

    public OutlineData CreateData()
    {
        Color arrowForcedColor = new Color(Graphics.ForcedColor.r, Graphics.ForcedColor.g, Graphics.ForcedColor.b, Graphics.ArrowOpacity);
        Color arrowHighlightedForcedColor = new Color(Graphics.ForcedColor.r, Graphics.ForcedColor.g, Graphics.ForcedColor.b, Graphics.ArrowOpacity * 0.34f);
        Color arrowSelectedColor = new Color(Graphics.DefaultColor.r, Graphics.DefaultColor.g, Graphics.DefaultColor.b, Graphics.ArrowOpacity);
        Color arrowHighlightedColor = new Color(arrowSelectedColor.r, arrowSelectedColor.g, arrowSelectedColor.b, Graphics.ArrowOpacity * 0.34f);

        Color outlineForcedColor = new Color(Graphics.ForcedColor.r, Graphics.ForcedColor.g, Graphics.ForcedColor.b, Graphics.OutlineOpacity);
        Color outlineHighlightedForcedColor = new Color(Graphics.ForcedColor.r, Graphics.ForcedColor.g, Graphics.ForcedColor.b, Graphics.OutlineOpacity * 0.34f);
        Color outlineSelectedColor = new Color(Graphics.DefaultColor.r, Graphics.DefaultColor.g, Graphics.DefaultColor.b, Graphics.OutlineOpacity);
        Color outlineHighlightedColor = new Color(outlineSelectedColor.r, outlineSelectedColor.g, outlineSelectedColor.b, Graphics.OutlineOpacity * 0.34f);

        return new OutlineData(
            arrowForcedColor,
            arrowHighlightedForcedColor,
            arrowSelectedColor,
            arrowHighlightedColor,

            outlineForcedColor,
            outlineHighlightedForcedColor,
            outlineSelectedColor,
            outlineHighlightedColor,

            arrowSprite: Graphics.ArrowSprite,
            outlineSprite: Graphics.OutlineSprite,
            sortingOrder: Graphics.SortingOrder,
            outlineOffset: OutlineOffset,
            arrowOffset: ArrowOffset,
            outlineSize: OutlineSize);
    }
}
