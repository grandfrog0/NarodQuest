using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _selectedRenderer;
    private DroppedItem _targetItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (_targetItem == null || Vector3.Distance(transform.position, _targetItem.transform.position) < Vector3.Distance(transform.position, collision.transform.position))
            {
                _targetItem = collision.GetComponent<DroppedItem>();
                _targetItem.PickUp();
            }
        }
    }

    public void OnSelectedItemChanged(Item item)
    {
        _selectedRenderer.gameObject.SetActive(item != null);
        _selectedRenderer.sprite = item.Icon;
    }
}
