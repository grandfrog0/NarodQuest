using UnityEngine;

public class InteractionPointViewer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _wrongPlaceColor;
    
    public void SetPlaceCorrect(bool value)
    {
        _renderer.color = value ? _defaultColor : _wrongPlaceColor;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
