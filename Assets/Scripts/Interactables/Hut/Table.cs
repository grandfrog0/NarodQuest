using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Table : InteractableObject, IDescriptable
{
    [SerializeField] private List<PlayerBringable> _bringables;
    [SerializeField] private List<ItemRenderer> _items;
    [SerializeField] private ItemRenderer _renderer;
    private Coroutine _observePlayerRoutine;

    public int Id => this.GetId();

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
                bringable.Renderer.SortingOrder = data.Player.transform.position.y > transform.position.y ? 1 : 0;
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

    public void OnPlayerEnter()
    {
        StopObserving();

        _observePlayerRoutine = StartCoroutine(ObservePlayerRoutine());
    }

    public void OnPlayerExit()
    {
        StopObserving();
    }

    private void StopObserving()
    {
        if (_observePlayerRoutine != null)
        {
            StopCoroutine(_observePlayerRoutine);
            _observePlayerRoutine = null;
        }
    }

    private IEnumerator ObservePlayerRoutine()
    {
        Transform player = BringableObjectController.Instance.transform;
        bool isPlayerHigher = false;

        while (true)
        {
            if (!isPlayerHigher && player.transform.position.y > transform.position.y)
            {
                _renderer.SortingOrder = 1;
                foreach (PlayerBringable bringable in _bringables)
                {
                    bringable.Renderer.SortingOrder = 1;
                }
                foreach (ItemRenderer itemRenderer in _items)
                {
                    itemRenderer.SortingOrder = 1;
                }

                isPlayerHigher = true;
            }
            else if (isPlayerHigher && player.transform.position.y < transform.position.y)
            {
                _renderer.SortingOrder = 0;
                foreach (PlayerBringable bringable in _bringables)
                {
                    bringable.Renderer.SortingOrder = 0;
                }
                foreach (ItemRenderer itemRenderer in _items)
                {
                    itemRenderer.SortingOrder = 0;
                }

                isPlayerHigher = false;
            }

            yield return new WaitForSeconds(0.75f);
        }
    }

    public List<string> GetTags()
    {
        return new List<string>
        {
            "bringables",
            string.Join(';', _bringables.Select(x => x.GetId())),
            "items",
            string.Join(';', _items.Select(x => x.GetId()))
        };
    }

    public void ApplyTags(List<string> tags)
    {
        int bringablesIndex = tags.IndexOf("bringables") + 1;
        if (bringablesIndex != 0 && bringablesIndex < tags.Count)
        {
            foreach (PlayerBringable bringable in _bringables)
            {
                bringable.Renderer.SortingOrder = 0;
                bringable.OnDrag -= OnItemDrag;
            }
            _bringables.Clear();


            foreach (string idString in tags[bringablesIndex].Split(';'))
            {
                _bringables.Add(ObjectRegistrator.Get(int.Parse(idString)).GetComponent<PlayerBringable>());
            }
            foreach (PlayerBringable bringable in _bringables)
            {
                bringable.Renderer.SortingOrder = 1;
                bringable.OnDrag += OnItemDrag;
            }
        }

        int itemsIndex = tags.IndexOf("items") + 1;
        if (itemsIndex != 0 && itemsIndex < tags.Count)
        {
            _items.Clear();

            foreach (string idString in tags[itemsIndex].Split(';'))
            {
                _items.Add(ObjectRegistrator.Get(int.Parse(idString)).GetComponent<ItemRenderer>());
            }
        }
    }
}
