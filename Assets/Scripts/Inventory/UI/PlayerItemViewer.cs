using UnityEngine;

public class PlayerItemViewer : MonoBehaviour
{
    private SpriteRenderer Renderer => _renderer ?? (_renderer = GetComponent<SpriteRenderer>());
    private SpriteRenderer _renderer;

    public void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        Renderer.sprite = sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
