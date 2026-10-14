using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemRenderer : MonoBehaviour
{
    public int SortingOrder
    {
        get => _sortingOrder;
        set
        {
            _sortingOrder = value;

            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].sortingOrder = _sortingOrders[i] + _sortingOrder;
            }
        }
    }
    [SerializeField] private int _sortingOrder;

    [SerializeField] private SpriteRenderer[] _renderers;
    [SerializeField] private int[] _sortingOrders;
}
