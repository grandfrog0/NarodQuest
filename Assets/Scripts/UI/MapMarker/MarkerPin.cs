using UnityEngine;
using UnityEngine.UI;

public class MarkerPin : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private RectTransform _rect;

    public void SetIcon(Sprite sprite) => _icon.sprite = sprite;
    public RectTransform Rect => _rect;
}
