using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _button;
    private UnityAction _onClick;

    public void Initialize(Sprite icon, int count, UnityAction onClick = null)
    {
        _iconImage.sprite = icon;
        _countText.text = FormatCount(count);

        _onClick = onClick;
        if (_onClick != null)
        {
            _button.onClick.AddListener(_onClick);
        }
    }

    private string FormatCount(int count)
    {
        return count > 1 ? count.ToString() : "";
    }
}
