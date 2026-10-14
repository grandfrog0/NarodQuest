using System;
using System.Collections.Generic;
using UnityEngine;

public class Table : InteractableObject
{
    [SerializeField] private List<PlayerBringable> _bringables;

    private void Start()
    {
        foreach (PlayerBringable bringable in _bringables)
        {
            bringable.Renderer.SortingOrder = 1;
            bringable.OnDrag += OnItemDrag;
        }
    }

    public override void Interact(InteractionData data)
    {
        BringableObjectController bringableController = data.BringableController;

        PlayerBringable bringable = bringableController.Current;

        // Если игрок несет предмет, который не стоит на столе
        if (bringable != null && !_bringables.Contains(bringable))
        {
            // То отпускаем предмет
            bringableController.Detach();

            // И ставим его на стол
            bringable.transform.position = data.Point;

            if (bringable.Renderer != null)
            {
                bringable.Renderer.SortingOrder = 1;
            }

            _bringables.Add(bringable);
            bringable.OnDrag += OnItemDrag;
        }
    }

    private void OnItemDrag(PlayerBringable bringable)
    {
        if (bringable.Renderer != null)
        {
            bringable.Renderer.SortingOrder = 0;
        }

        bringable.OnDrag -= OnItemDrag;
        _bringables.Remove(bringable);
    }
}
