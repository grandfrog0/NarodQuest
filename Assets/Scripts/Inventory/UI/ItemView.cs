using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _countText;
    [SerializeField] private Button _button;
    private UnityAction _onClick;

    public ItemCountPair ItemCountPair { get; private set; }

    public void Initialize(ItemCountPair itemCountPair, UnityAction onClick = null)
    {
        ItemCountPair = itemCountPair;

        _iconImage.sprite = itemCountPair.Item.Icon;
        _countText.text = FormatCount(itemCountPair.Count);

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
