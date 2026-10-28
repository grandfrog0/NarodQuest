using System.Collections;
using UnityEngine;

public class OutlineView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public void Initalize(OutlineConfig config)
    {
        _renderer.sprite = config.Sprite;
        _renderer.color = config.Color;
        _renderer.sortingOrder = config.SortingOrder;

        transform.localPosition = config.Offset;
        transform.localScale = config.Size;
    }
}
